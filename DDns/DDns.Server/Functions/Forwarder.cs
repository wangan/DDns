using DDns.Comm;

namespace DDns.Server.Functions {
    public class Forwarder {
        private SimpleTunnel _tunnel = null;

        public Forwarder(SimpleTunnel tunnel) {
            _tunnel = tunnel;
        }

    }
}
