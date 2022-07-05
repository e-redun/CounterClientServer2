using CounterLib.Services;
using System;

namespace CounterLib.Connections.Sockets
{
    /// <summary>
    /// Класс Socket - коннектора со стороны Клиента
    /// </summary>
    public class SocketClient : BaseClient
    {
        public SocketClient(ConnectedObject connector) : base(connector)
        {
        }

        public override void Connect()
        {
            try
            {
                ((SocketConnectedObject)Connector).Socket = ConnectionHelper.CreateSocket();

                // подключение к удаленному хосту
                ((SocketConnectedObject)Connector).Socket.Connect(ConnectionHelper.EndPoint());
            }
            catch (Exception ex)
            {
                Print?.Invoke("метод Connect " + ex.Message);
            }
        }
    }
}