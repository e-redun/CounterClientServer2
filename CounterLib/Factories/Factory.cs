using CounterLib.Connections;
using CounterLib.Enums;

namespace CounterLib.Factories
{
    public static class Factory
    {
        /// <summary>
        /// Создает/возвращает клиента согласно протоколу
        /// </summary>
        /// <param name="protocol">Протокол</param>
        /// <returns>Клиент</returns>
        public static BaseClient CreateClient(ConProtocols protocol)
        {
            IProtocolFactory protocolFactory = CreateProtocolFactory(protocol);

            return protocolFactory.CreateClient();
        }

        /// <summary>
        /// Создает/возвращает сервер согласно протоколу
        /// </summary>
        /// <param name="protocol">Протокол</param>
        /// <returns>Сервер</returns>
        public static BaseServer CreateServer(ConProtocols protocol)
        {
            IProtocolFactory protocolFactory = CreateProtocolFactory(protocol);

            return protocolFactory.CreateServer();
        }


        /// <summary>
        /// Создает/возвращает клиентский коннектор согласно протоколу
        /// </summary>
        /// <param name="protocol"></param>
        /// <returns>Клиентский коннектор</returns>
        public static ConnectedObject CreateClientConnector(ConProtocols protocol)
        {
            IProtocolFactory protocolFactory = CreateProtocolFactory(protocol);

            return protocolFactory.CreateClientConnector();
        }


        /// <summary>
        /// Создает/возвращает "протокольную" фабрику
        /// </summary>
        /// <param name="protocol">Протокол</param>
        /// <returns>"Протокольная" фабрика</returns>
        public static IProtocolFactory CreateProtocolFactory(ConProtocols protocol)
        {
            IProtocolFactory output = null;

            switch (protocol)
            {
                case ConProtocols.Socket:
                    output = new SocketFactory();
                    break;

                case ConProtocols.Tcp:
                    output = new TcpFactory();
                    break;

                case ConProtocols.WebSoket:
                    break;
            }

            return output;
        }
    }
}
