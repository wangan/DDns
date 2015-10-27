namespace DDns.Comm {

    /// <summary>
    /// 端与端之间的通信通道
    /// </summary>
    public interface ITunnel {

        /// <summary>
        /// 通道的标识
        /// </summary>
        string Id { get; }

        /// <summary>
        /// 开启本次通信过程
        /// </summary>
        void Begin();

        /// <summary>
        /// 结束本次通信
        /// </summary>
        void End();

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="buffer"></param>
        void Send(byte[] buffer);

        /// <summary>
        /// 接收指定长度的数据
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>

        byte[] Receive(int length);
    }
}
