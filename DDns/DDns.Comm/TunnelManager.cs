using System.Collections.Generic;

namespace DDns.Comm {

    /// <summary>
    /// 管理系统中所有的通信通道
    /// </summary>
    public class TunnelManager {
        private Dictionary<string, ITunnel> _tables = new Dictionary<string, ITunnel>();

        /// <summary>
        /// 获取当前系统中开启的所有通信通道
        /// </summary>
        public ICollection<ITunnel> Tunnels { get; private set; }

        public TunnelManager() {
            Tunnels = new List<ITunnel>();
        }

        public void Manage(ITunnel tunnel) {
            Tunnels.Add(tunnel);
            _tables.Add(tunnel.Id, tunnel);
        }

        public ITunnel Get(string id) {
            ITunnel tunnel = null;
            if (_tables.TryGetValue(id, out tunnel))
                return tunnel;

            return null;
        }
    }
}
