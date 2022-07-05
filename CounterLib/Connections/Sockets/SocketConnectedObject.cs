using System;
using System.Net.Sockets;
using System.Text;

namespace CounterLib.Connections.Sockets
{
    /// <summary>
    /// Socket-коннектор
    /// </summary>
    public class SocketConnectedObject : ConnectedObject
    {
        /// <summary>
        /// Сокет
        /// </summary>
        public Socket Socket { get; set; }

        /// <summary>
        /// Отправляет OutgoingMessage на подключенный сокет
        /// </summary>
        public override void SendOutgoingMessage()
        {
            OutgoingMessage = OutgoingMessage + MessageTerminator;

            Socket.Send(Encoding.Unicode.GetBytes(OutgoingMessage));
        }

        /// <summary>
        /// Загружает из сокета полученное сообщение
        /// </summary>
        public override void SetIncomingMessage()
        {
            int bytes;

            byte[] buffer = new byte[BufferSize];

            MessageBuilder.Clear();

            do
            {
                // количество полученных байт
                bytes = Socket.Receive(buffer, buffer.Length, 0);

                MessageBuilder.Append(Encoding.Unicode.GetString(buffer, 0, bytes));
            }
            while (!MessageReceived());

            IncomingMessage = MessageBuilder.ToString();

            // удаление терминатора
            IncomingMessage = IncomingMessage.Replace(MessageTerminator,"");
        }

        /// <summary>
        /// Возвращает True, если сообщение получено полностью
        /// </summary>
        /// <returns></returns>
        public bool MessageReceived()
        {
            return MessageBuilder.ToString().IndexOf(MessageTerminator) > -1;
        }

        /// <summary>
        /// Возвращает True, если сокет подключен к удаленному узлу
        /// </summary>
        /// <returns></returns>
        public override bool Connected()
        {
            if (Socket == null)
            {
                return false;
            }

            return Socket.Connected;
        }

        /// <summary>
        /// Закрывает соединение
        /// </summary>
        public override void Close()
        {
            try
            {
                Socket.Shutdown(SocketShutdown.Both);
                Socket.Close();
            }
            catch (Exception)
            {
            }
        }

        public override void Process()
        {}
    }
}