using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Lawl.Networking.EventArgs;
using Lawl.Networking.FrameworkHelper;
using Lawl.Networking.Packets.Answer.Interface;
using Lawl.Networking.Packets.Base;
using Lawl.Networking.Packets.Request.Interface;

namespace Lawl.Networking
{
    public class LocalServerLocator<TRequest, TAnswer> where TRequest : PacketBase, IAcknowledgeRequestPacket
                                                       where TAnswer : PacketBase, IAcknowledgedPacket
    {
        public EventHandler<PacketStream> OnServerFound { get; set; }

        private readonly int _port;
        private readonly TRequest _acknowledgeRequestPacket;
        private readonly int _timeout;
        private readonly INetworkBrowser _networkBrowser;

        public LocalServerLocator(int port, TRequest acknowledgeRequestPacket, int timeout = 5, INetworkBrowser browser = null)
        {
            _port = port;
            _acknowledgeRequestPacket = acknowledgeRequestPacket;
            _timeout = timeout;
            _networkBrowser = browser ?? new DefaultNetworkBrowser();
        }

        public virtual void GetLocalAsync()
        {
            var networkComputers = _networkBrowser.GetNetworkComputers();
            foreach (var ip in networkComputers.SelectMany(Dns.GetHostAddresses).Where(ip=>ip.AddressFamily != AddressFamily.InterNetworkV6))
            {
                var packetStream = new PacketStream(new IPEndPoint(ip, _port));
                packetStream.OnPacketStreamConnected += OnPacketStreamConnected;
                packetStream.ConnectAsync();
            }
        }

        private void OnPacketStreamConnected(object sender, OnPacketStreamConnectedEventArgs args)
        {
            var stream = (PacketStream) sender;
            stream.RegisterPacket<TRequest>();
            stream.RegisterPacket<TAnswer>();
            stream.WritePacket(_acknowledgeRequestPacket);
            var now = DateTime.Now;
            while (DateTime.Now.Subtract(now).Seconds < _timeout && !CheckForAnswer(stream))
            {
                Thread.Sleep(50);
            }
        }

        private bool CheckForAnswer(PacketStream stream)
        {
            if (stream.Pending)
            {
                var answer = stream.ReadPacket() as TAnswer;
                if (answer != null && answer.ProtocolName == _acknowledgeRequestPacket.ProtocolName && OnServerFound != null)
                {
                    OnServerFound(this, stream);
                    return true;
                }
            }
            return false;
        }
    }
}
