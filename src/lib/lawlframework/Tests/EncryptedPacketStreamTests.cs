using System.Linq.Expressions;
using System.Net;
using System.Security.Cryptography;
using System.Threading;
using Lawl.Networking;
using Lawl.Security;
using NUnit.Framework;

namespace Tests
{
    public class EncryptedPacketStreamTests : PacketStreamTests
    {
        public EncryptedPacketStreamTests()
        {
            var symmetricAlgorithmProvider = new SymmetricAlgorithmProvider<AesCryptoServiceProvider>();
            symmetricAlgorithmProvider.GenerateIV();
            symmetricAlgorithmProvider.GenerateKey();

            Server = new PacketStream(new IPEndPoint(IPAddress.Parse("127.0.0.3"), 1337), symmetricAlgorithmProvider);
            Server.RegisterPacket<TestPacket>();

            Client = new PacketStream(new IPEndPoint(IPAddress.Parse("127.0.0.3"), 1337), symmetricAlgorithmProvider);
            Client.RegisterPacket<TestPacket>();
        }


        [Test]
        public override void TestPacketStream()
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
    }
}