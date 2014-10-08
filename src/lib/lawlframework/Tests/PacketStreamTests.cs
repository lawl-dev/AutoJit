using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Lawl.Networking;
using Lawl.Networking.Packets.Base;
using Lawl.Networking.Packets.Attribute;
using Lawl.Networking.EventArgs;


namespace Tests
{
    public class PacketStreamTests
    {
        public PacketStream Server { get; set; }
        public PacketStream Client { get; set; }

        public PacketStreamTests()
        {
            Server = new PacketStream(new IPEndPoint(IPAddress.Parse("127.0.0.3"), 1337));
            Server.RegisterPacket<TestPacket>();
            Client = new PacketStream(new IPEndPoint(IPAddress.Parse("127.0.0.3"), 1337));
            Client.RegisterPacket<TestPacket>();
        }


        [Test]
        public virtual void TestPacketStream()
        {
            Server.OnPacketStreamConnected += OnClientConnected;
            Server.ListenForClientAsync();
            Client.OnPacketStreamConnected += OnClientConnectedSuccessToServer;
            Client.ConnectAsync();
            while (true)
            {
                Thread.Sleep(100);
            }
        }

        protected void OnClientConnectedSuccessToServer(object sender, OnPacketStreamConnectedEventArgs e)
        {
            var client = (PacketStream) sender;
            client.WritePacket(new TestPacket("Hey Server!"));
            while (true)
            {
                if (client.Pending)
                {
                    var testPacket = (TestPacket) client.ReadPacket();
                    Console.WriteLine("Recieved Packet from Server: {0}", testPacket.Message);
                    client.WritePacket(new TestPacket("Hey Server!")
                    {
                        Bool = true,
                        PacketGuid = Guid.NewGuid().ToString()
                    });
                    //, Ints = new List<int>() { 1, 2, 3 }, Float = 0.03f, Chars = new List<char>() { 'a', 'a', 'a', 'a' }, CharArray = new char[]{'a', 'b'}});
                }
            }
        }

        protected void OnClientConnected(object sender,
            OnPacketStreamConnectedEventArgs onPacketStreamConnectedEventArgs)
        {
            var server = (PacketStream) sender;
            while (true)
            {
                if (server.Pending)
                {
                    var testPacket = (TestPacket) server.ReadPacket();
                    Console.WriteLine("Recieved Message from Client: {0}", testPacket.Message);
                    server.WritePacket(new TestPacket("Hey Client!"));
                }
            }
        }



        //Sample Packet
        public class TestPacket : PacketBase
        {
            public TestPacket(string message)
            {
                PacketGuid = Guid.NewGuid().ToString();
                Message = message;
            }

            public override int PacketType
            {
                get { return 1; }
            }

            [PropertyIndex(0)]
            public override string PacketGuid { get; set; }

            [PropertyIndex(1)]
            public string Message { get; set; }

            [PropertyIndex(2)]
            public float Float { get; set; }

            [PropertyIndex(3)]
            public bool Bool { get; set; }
        }
    }
}

