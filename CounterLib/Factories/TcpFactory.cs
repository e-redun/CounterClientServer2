using CounterLib.Connections;
using CounterLib.Connections.Sockets;
using CounterLib.Connections.Tcp;

namespace CounterLib.Factories
{
    public class TcpFactory : IProtocolFactory
    {
        /// <summary>
        /// Создает и возвращает TCPClient
        /// </summary>
        /// <returns>TCPClient</returns>
        public BaseClient CreateClient()
        {
            return new TCPClient(Factory.CreateClientConnector(Enums.ConProtocols.Tcp));
        }

        /// <summary>
        /// Создает и возвращает клиентский TCPClient-коннектор
        /// </summary>
        /// <returns>TCPClient-коннектор</returns>
        public ConnectedObject CreateClientConnector()
        {
            return new TcpConnectedObject();
        }

        /// <summary>
        /// Создает и возвращает TCPClient-сервер
        /// </summary>
        /// <returns>TCPClient-сервер</returns>
        public BaseServer CreateServer()
        {
            return new TcpServer();
        }
    }
}