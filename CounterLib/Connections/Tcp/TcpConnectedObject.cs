using System.Net.Sockets;
using System.Text;

namespace CounterLib.Connections.Tcp
{
    /// <summary>
    /// Tcp-коннектор
    /// </summary>
    public class TcpConnectedObject : ConnectedObject
    {
        /// <summary>
        /// TcpClient
        /// </summary>
        public TcpClient TcpClient { get; set; }

        /// <summary>
        /// Поток
        /// </summary>
        public NetworkStream Stream { get; set; }


        /// <summary>
        /// Пишет исходяещее сообщение в поток
        /// </summary>
        public override void SendOutgoingMessage()
        {
            byte[] buffer = Encoding.Unicode.GetBytes(OutgoingMessage);

            Stream.Write(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// Загружает из потока входящее текстовое сообщение
        /// </summary>
        public override void SetIncomingMessage()
        {
            int bytes = 0;

            byte[] buffer = new byte[BufferSize];

            MessageBuilder.Clear();

            do
            {
                bytes = Stream.Read(buffer, 0, buffer.Length);

                MessageBuilder.Append(Encoding.Unicode.GetString(buffer, 0, bytes));
            }
            while (Stream.DataAvailable);

            IncomingMessage = MessageBuilder.ToString();
        }

        /// <summary>
        /// Возвращает True, если сокет TcpClient-а подключен к удаленному узлу
        /// </summary>
        /// <returns></returns>
        public override bool Connected()
        {
            if (TcpClient == null)
            {
                return false;
            }

            return TcpClient.Connected;
        }

        /// <summary>
        /// Закрывает соединение
        /// </summary>
        public override void Close()
        {
            //отключение потока
            if (Stream != null)
            {
                Stream.Close();
            }

            //отключение клиента
            if (TcpClient != null)
            {
                TcpClient.Close();
            }
        }

        public override void Process()
        {}
    }
}