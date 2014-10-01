using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Lawl.Networking.EventArgs
{
    public class OnPacketStreamConnectedEventArgs : System.EventArgs
    {
        public NetworkStream NetworkStream { get; set; }

        public OnPacketStreamConnectedEventArgs(NetworkStream networkStream)
        {
            NetworkStream = networkStream;
        }
    }
}
