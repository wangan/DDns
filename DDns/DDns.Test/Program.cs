using System.Net;
using System.Net.Sockets;

namespace DDns.Test {
    class Program {
        static void Main(string[] args) {

            Socket client = new Socket(SocketType.Stream, ProtocolType.Tcp);
            DnsEndPoint dnsEndPoint = new DnsEndPoint("dev.com", 9876);
            client.Connect(dnsEndPoint);
        }
    }
}
