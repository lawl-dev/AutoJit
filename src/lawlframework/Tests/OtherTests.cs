using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Lawl.Networking;
using Lawl.Networking.Packets.Base;
using NUnit.Framework;

namespace Tests
{
    public class OtherTests
    {
        public class UdpStream : Stream
        {
            private readonly MemoryStream _memoryStream;
            private readonly IPEndPoint _endPoint;
            private readonly UdpClient _udpClient;

            public UdpStream(MemoryStream memoryStream, IPEndPoint endPoint)
            {
                _memoryStream = memoryStream;
                _endPoint = endPoint;
                _udpClient = new UdpClient(endPoint);
            }

            public override void Flush()
            {
                throw new NotImplementedException();
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                throw new NotImplementedException();
            }

            public override void SetLength(long value)
            {
                throw new NotImplementedException();
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                return _memoryStream.Read(buffer, offset, count);
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                _udpClient.Send(buffer, count);
            }

            public override bool CanRead
            {
                get { throw new NotImplementedException(); }
            }

            public override bool CanSeek
            {
                get { throw new NotImplementedException(); }
            }

            public override bool CanWrite
            {
                get { throw new NotImplementedException(); }
            }

            public override long Length
            {
                get { throw new NotImplementedException(); }
            }

            public override long Position { get; set; }
        }
        public class UDPService
        {
            private readonly UdpClient _client;
            public Dictionary<IPEndPoint, Stream> Streams { get; set; }

            public UDPService(UdpClient client)
            {
                _client = client;
                Task.Factory.StartNew(Reciever);
            }

            private void Reciever()
            {
                while (true)
                {
                    if (_client.Available > 0)
                    {
                        IPEndPoint endpoint = null;
                        var receive = _client.Receive(ref endpoint);
                        if (!Streams.ContainsKey(endpoint))
                        {
                            Streams.Add(endpoint, new MemoryStream());
                        }
                        Streams[endpoint].Write(receive, 0, receive.Length);
                    }
                }
            }

            public virtual Stream GetStream(IPEndPoint endPoint)
            {
                if (!Streams.ContainsKey(endPoint))
                {
                    Streams.Add(endPoint, new MemoryStream());
                }
                return Streams[endPoint];
            }
        }


        [Test]
        public void Test()
        {
            var udpService = new UDPService(new UdpClient(1337));
            var stream = udpService.GetStream(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1337));
            var stream2 = udpService.GetStream(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1338));

            var packetStream = new PacketStream(stream);
            var packetStream2 = new PacketStream(stream2);
        }
    }
}