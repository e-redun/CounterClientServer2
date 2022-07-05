using CounterLib.Connections;
using CounterLib.Connections.Sockets;
using CounterLib.Enums;

namespace CounterLib.Factories
{
    public class SocketFactory : IProtocolFactory
    {
        /// <summary>
        /// Создает и возвращает Socket-клиента
        /// </summary>
        /// <returns>Socket-клиент</returns>
        public BaseClient CreateClient()
        {
            return new SocketClient(Factory.CreateClientConnector(ConProtocols.Socket));
        }

        /// <summary>
        /// Создает и возвращает Socket-сервер
        /// </summary>
        /// <returns>Socket-сервер</returns>
        public BaseServer CreateServer()
        {
            return new SocketServer();
        }

        /// <summary>
        /// Создает и возвращает клиентский Socket-коннектор
        /// </summary>
        /// <returns>Socket-коннектор</returns>
        public ConnectedObject CreateClientConnector()
        {
            return new SocketConnectedObject();
        }
    }
}