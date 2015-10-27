using System;
using System.Net.Sockets;

namespace DDns.Comm {
    public class SimpleTunnel : ITunnel {
        private string _id = string.Empty;
        public string Id {
            get {
                return _id;
            }
        }

        public Socket Socket { get; private set; }

        public SimpleTunnel(Socket socket) {
            _id = Guid.NewGuid().ToString("N");
            Socket = socket;
        }

        public void Begin() {
            throw new NotImplementedException();
        }

        public void End() {
            throw new NotImplementedException();
        }

        public byte[] Receive(int length) {
            throw new NotImplementedException();
        }

        public void Send(byte[] buffer) {
            throw new NotImplementedException();
        }
    }
}
