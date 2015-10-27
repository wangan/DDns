using DDns.Comm;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace DDns.Server {
    public class DDnsServer {
        private Socket _server = null;
        private EndPoint _endPoint = null;
        private bool _isDispose = false;
        private static ManualResetEvent Next = new ManualResetEvent(true);
        private static TunnelManager _tunnelManager = new TunnelManager();

        public DDnsServer(string addressOrHostname, int port) {
            IPAddress ipAddress = null;
            if (IPAddress.TryParse(addressOrHostname, out ipAddress)) {
                _endPoint = new IPEndPoint(ipAddress, port);
            }
        }

        public void Start() {
            _server = new Socket(SocketType.Stream, ProtocolType.Tcp);
            _server.Bind(_endPoint);
            _server.Listen(100);


            do {
                Next.Reset();

                SocketAsyncEventArgs args = new SocketAsyncEventArgs();
                args.Completed += new EventHandler<SocketAsyncEventArgs>(Accepted);

                _server.AcceptAsync(args);

                bool success = Next.WaitOne(5000);
                if (!success) {
                    args.Dispose();
                }
            } while (!_isDispose);
        }

        public void Accepted(object sender, SocketAsyncEventArgs e) {
            Next.Set();

            SimpleTunnel tunnel = new SimpleTunnel(e.AcceptSocket);
            _tunnelManager.Manage(tunnel);

            Socket mySocket = e.AcceptSocket;
            //将请求转化成字节数组！
            // 为读取数据而准备缓存
            Byte[] bReceive = new Byte[1024];
            int i = mySocket.Receive(bReceive, bReceive.Length, 0);

            //转换成字符串类型
            string sBuffer = Encoding.ASCII.GetString(bReceive);
            Console.WriteLine(sBuffer);

            // 查找 "HTTP" 的位置
            int iStartPos = sBuffer.IndexOf("HTTP", 1);
            string sHttpVersion = sBuffer.Substring(iStartPos, 8);
            String sMimeType = "text/html";

            {

                var text = "Server Say : OK !";
                var cBuffer = Encoding.ASCII.GetBytes(text);
                sBuffer = "";

                sBuffer = sBuffer + sHttpVersion + " 200 OK" + "\r\n";
                sBuffer = sBuffer + "Server: dev.com\r\n";
                sBuffer = sBuffer + "Content-Type: " + sMimeType + "\r\n";
                sBuffer = sBuffer + "Accept-Ranges: bytes\r\n";
                sBuffer = sBuffer + "Content-Length: " + cBuffer.Length + "\r\n\r\n";

                Byte[] bSendData = Encoding.ASCII.GetBytes(sBuffer);
                //发送
                e.AcceptSocket.Send(bSendData);
                e.AcceptSocket.Send(cBuffer);

            }

        }
    }
}
