using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Threading;
using NUnit.Framework;
using Lawl.Networking;
using Lawl.Networking.Packets.Base;
using Lawl.Networking.Packets.Attribute;
using Lawl.Networking.Packets.Request.Interface;
using Lawl.Networking.Packets.Answer.Interface;
using Lawl.Networking.EventArgs;
using Lawl.Networking.FrameworkHelper;


namespace Tests
{
    public class LocalServerLocatorTests
    {
        private LocalServerLocator<AcknowledgeRequestPacket, AcknowledgePacket> _localServerLocator;
        public PacketStream Server { get; set; }
        private bool _testSuccess;
        private const string ServerIp = "127.0.0.1";
        private const int Timeout = 15;

        public LocalServerLocatorTests()
        {
            ServerLocatorSetup();
            ServerSetup();
        }



        [Test]
        public void Test()
        {
            _localServerLocator.GetLocalAsync();
            Server.ListenForClientAsync();
            DateTime now = DateTime.Now;
            while (!_testSuccess && now.Subtract(DateTime.Now).Seconds < Timeout)
            {
                Thread.Sleep(100);
            }
            Assert.IsTrue(_testSuccess);
        }

        private void ServerSetup()
        {
            Server = new PacketStream(new IPEndPoint(IPAddress.Parse(ServerIp), 1339));
            Server.RegisterPacket<AcknowledgeRequestPacket>();
            Server.OnPacketStreamConnected += OnClientConnected;
        }

        private void ServerLocatorSetup()
        {
            var requestPacket = new AcknowledgeRequestPacket()
            {
                PacketGuid = Guid.NewGuid().ToString(),
                ProtocolName = "Lawls1337Protocol"
            };
            var networkBrowserMock = new MyNetworkBrowserMock();
            _localServerLocator = new LocalServerLocator<AcknowledgeRequestPacket, AcknowledgePacket>(1339, requestPacket, Timeout, networkBrowserMock);
            _localServerLocator.OnServerFound += OnServerFound;
        }

        private void OnServerFound(object sender, PacketStream stream)
        {
            Console.WriteLine("Server found!");
            _testSuccess = true;
        }

        private void OnClientConnected(object sender, OnPacketStreamConnectedEventArgs args)
        {
            var stream = (PacketStream) sender;
            while (true)
            {
                if (stream.Pending)
                {
                    var acknowledgeRequestPacket = stream.ReadPacket() as IAcknowledgeRequestPacket;
                    if (acknowledgeRequestPacket != null)
                    {
                        stream.WritePacket(new AcknowledgePacket() { PacketGuid = Guid.NewGuid().ToString(), ProtocolName = "Lawls1337Protocol" });
                    }
                }
            }
        }

        public class AcknowledgeRequestPacket : PacketBase, IAcknowledgeRequestPacket
        {
            public override int PacketType
            {
                get { return 1; }
            }
            [PropertyIndex(0)]
            public override string PacketGuid { get; set; }

            [PropertyIndex(1)]
            public List<string> List { get; set; }

            [PropertyIndex(2)]
            public string ProtocolName { get; set; }
        }

        private class AcknowledgePacket : PacketBase, IAcknowledgedPacket
        {
            public override int PacketType
            {
                get { return 2; }
            }
            [PropertyIndex(0)]
            public override string PacketGuid { get; set; }

            [PropertyIndex(1)]
            public List<string> List { get; set; }

            [PropertyIndex(2)]
            public string ProtocolName { get; set; }
            }
        }

        public class MyNetworkBrowserMock : INetworkBrowser
        {
            public List<string> GetNetworkComputers()
            {
                return new List<string> {"localhost"};
            }
        }
    }