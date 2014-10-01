using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using Lawl.Networking.EventArgs;
using Lawl.Networking.Packets.Base;
using Lawl.Reflection;
using Lawl.Security;

namespace Lawl.Networking
{
    public class PacketStream : IDisposable
    {
        public virtual List<Type> RegisteredPackets
        {
            get { return _registeredPackets.Select(x => x.Value).ToList(); }
        }

        private Dictionary<int, Type> _registeredPackets;

        
        public virtual EventHandler<OnPacketStreamConnectedEventArgs> OnPacketStreamConnected { get; set; }


        protected readonly IPEndPoint IpEndPoint;
        

        private Stream _stream;
        protected Stream UnderlyingStream
        {
            get { return _stream; }
            private set
            {
                _stream = value;
                Reader = new BinaryReader(_stream);
                Writer = new BinaryWriter(_stream);
            }
        }

        protected BinaryWriter Writer;
        protected BinaryReader Reader;
        protected readonly ISymmetricAlgorithmProvider SymmetricAlgorithmProvider;
        protected bool EncryptionModeOn
        {
            get { return SymmetricAlgorithmProvider != null; }
        }


        public PacketStream(Stream underlyingStream) {
            UnderlyingStream = underlyingStream;
            Writer = new BinaryWriter(UnderlyingStream);
            Reader = new BinaryReader(UnderlyingStream);
            Initialize();
        }
        
        public PacketStream(Stream underlyingStream, ISymmetricAlgorithmProvider symmetricAlgorithmProvider) {
            SymmetricAlgorithmProvider = symmetricAlgorithmProvider;
            UnderlyingStream = underlyingStream;
            Initialize();
        }

        public PacketStream(IPEndPoint ipEndPoint)
        {
            IpEndPoint = ipEndPoint;
            Initialize();
        }

        public PacketStream(IPEndPoint ipEndPoint, ISymmetricAlgorithmProvider symmetricAlgorithmProvider)
        {
            IpEndPoint = ipEndPoint;
            SymmetricAlgorithmProvider = symmetricAlgorithmProvider;
            Initialize();
        }

        
        public virtual async void ConnectAsync()
        {
            CheckConnectPosibble();
            var tcpClient = new TcpClient();
            await tcpClient.ConnectAsync(IpEndPoint.Address, IpEndPoint.Port);
            UnderlyingStream = tcpClient.GetStream();
            TriggerConnectedEvent();
        }


        public virtual void Connect()
        {
            var tcpClient = new TcpClient();
            tcpClient.Connect(IpEndPoint.Address, IpEndPoint.Port);
            UnderlyingStream = tcpClient.GetStream();
        }

        public virtual void ListenForClientAsync()
        {
            var tcpListener = new TcpListener(IpEndPoint);
            tcpListener.Start();
            tcpListener.BeginAcceptTcpClient(delegate(IAsyncResult ar)
            {
                UnderlyingStream = ((TcpListener) ar.AsyncState).EndAcceptTcpClient(ar).GetStream();
                TriggerConnectedEvent();
            }, tcpListener);
        }

        public virtual void ListenForClient()
        {
            var tcpListener = new TcpListener(IpEndPoint);
            tcpListener.Start();
            while (!tcpListener.Pending())
            {
                var client = tcpListener.AcceptTcpClient();
                UnderlyingStream = client.GetStream();
            }
        }


        public virtual void WritePacket(PacketBase packet) {
            var packetBytes = packet.ToByteArray();

            if (EncryptionModeOn)
            {
                WriteEncryptedPacket(packetBytes);
            }
            else
            {
                Writer.Write(packetBytes);   
            }

            Writer.Flush();
            UnderlyingStream.Flush();
        }

        private void WriteEncryptedPacket(byte[] packetBytes)
        {
            byte[] data;
            using (var ms = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(ms, SymmetricAlgorithmProvider.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(packetBytes, 0, packetBytes.Length);
                    cryptoStream.FlushFinalBlock();
                }
                data = ms.ToArray();
            }
            Writer.Write(data.Length);
            Writer.Write(data);
        }


        public virtual PacketBase ReadPacket() {
            if (EncryptionModeOn)
            {
                return ReadEncryptedPacket();
            }

            int packetType = Reader.ReadInt32();
            CheckPacketType(packetType);
            PacketBase packet = CreatePacketInstanceOf(packetType);
            return packet.Read(UnderlyingStream);
        }

        private PacketBase ReadEncryptedPacket()
        {
            PacketBase packet;
            var length = Reader.ReadInt32();

            byte[] buffer = Reader.ReadBytes(length);

            using (var memoryStream = new MemoryStream(buffer))
            {
                using (
                    var cryptoStream = new CryptoStream(memoryStream, SymmetricAlgorithmProvider.CreateDecryptor(),
                        CryptoStreamMode.Read))
                {
                    var plainBytes = new byte[buffer.Length];
                    cryptoStream.Read(plainBytes, 0, plainBytes.Length);

                    using (var ms = new MemoryStream(plainBytes))
                    {
                        using (var binaryReader = new BinaryReader(ms))
                        {
                            var packetType = binaryReader.ReadInt32();
                            CheckPacketType(packetType);
                            packet = CreatePacketInstanceOf(packetType);
                            packet = packet.Read(ms);
                        }
                    }
                }
            }
            return packet;
        }


        public virtual bool Pending
        {
            get
            {
                var stream = UnderlyingStream as NetworkStream;
                if (stream != null)
                {
                    return stream.DataAvailable;
                }

                return UnderlyingStream.Length != UnderlyingStream.Position;
            }
        }

        public virtual void RegisterPacket<T>() where T : PacketBase
        {
            RegisterPacket(typeof(T));
        }

        public virtual void RegisterPacket<T>(T value) where T : PacketBase
        {
            RegisterPacket(value.GetType());
        }
        

        public void RegisterPacket(Type t)
        {
            if (t.IsAssignableFrom(typeof (PacketBase)) && t != typeof(PacketBase))
                throw new Exception("Packet doesnt inherit from PacketBast!");


            var packet = t.CreateInstanceWithDefaultParameters<PacketBase>();
            _registeredPackets.Add(packet.PacketType, t);
        }

        public virtual void UnregisterPacket<T>() where T : PacketBase
        {
            UnregisterPacket(typeof(T));
        }

        public virtual void UnregisterPacket<T>(T value) where T : PacketBase
        {
            UnregisterPacket(value.GetType());
        }

        public virtual void UnregisterPacket(Type t)
        {
            if (t.BaseType != typeof (PacketBase))
                throw new Exception("Packet doesnt inerhit from PacketBast!");


            var packet = t.CreateInstanceWithDefaultParameters<PacketBase>();
            _registeredPackets.Remove(packet.PacketType);
        }


        public virtual void Dispose()
        {
            UnderlyingStream.Dispose();
            if (EncryptionModeOn)
            {
                Reader.Dispose();
                Reader.Dispose();
            }
        }

        protected void Initialize()
        {
            _registeredPackets = new Dictionary<int, Type>();
        }

        protected void CheckConnectPosibble()
        {
            if (IpEndPoint == null)
            {
                throw new InvalidOperationException("No IPEndPoint!");
            }
        }


        protected void TriggerConnectedEvent()
        {
            if (OnPacketStreamConnected != null)
            {
                OnPacketStreamConnected(this, new OnPacketStreamConnectedEventArgs((NetworkStream)UnderlyingStream));
            }
        }


        protected PacketBase CreatePacketInstanceOf(int packetType)
        {
            return _registeredPackets[packetType].CreateInstanceWithDefaultParameters<PacketBase>();
        }

        
        protected void CheckPacketType(int packetType)
        {
            if (!_registeredPackets.ContainsKey(packetType))
            {
                throw new Exception("Unregistered packet recieved!");
            }
        }
    }
}
