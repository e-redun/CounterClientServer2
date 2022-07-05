using CounterLib.Connections;

namespace CounterLib.Factories
{
    public interface IProtocolFactory
    {
        /// <summary>
        /// Создает и возвращает клиента
        /// </summary>
        /// <returns>Клиент</returns>
        BaseClient CreateClient();

        /// <summary>
        /// Создает и возвращает клиентский коннектор
        /// </summary>
        /// <returns>Клиентский коннектор</returns>
        ConnectedObject CreateClientConnector();

        /// <summary>
        /// Создает и возвращает сервер
        /// </summary>
        /// <returns>Сервер</returns>
        BaseServer CreateServer();
    }
}
