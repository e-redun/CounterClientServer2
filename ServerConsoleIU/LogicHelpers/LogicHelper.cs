using CounterLib;
using CounterLib.Connections;
using CounterLib.Enums;
using CounterLib.Factories;
using CounterLib.Models;
using CounterLib.Services;
using System;
using System.Threading.Tasks;

namespace ServerConsoleIU.LogicHelpers
{
    public class LogicHelper : ILogicHelper
    {
        ICounter counter = new CounterModel();

        /// <summary>
        /// Запускает сервер
        /// </summary>
        /// <param name="protocol">Протокол</param>
        /// <returns>Задача (Task) - прослушивание порта</returns>
        public Task RunServer(ConProtocols protocol)
        {
            Task output = null;

            BaseServer server;

            // получение настроек сервера
            ServerSettingsModel serverSettings = GetSettings(protocol);

            // валидация настроек сервера
            bool valid = ValidateServerSettings(serverSettings);

            if (valid)
            {
                ConnectionHelper connectionHelper = new ConnectionHelper();
                connectionHelper.GetSettings(serverSettings);

                // создание сервера
                server = Factory.CreateServer(protocol);
                server.Counter = counter;
                server.SubscibeOnCounter();
                server.ConnectionHelper = connectionHelper;

                server.Print += (mes) => Console.WriteLine(mes);
                server.ClearAndPrint += (mes1, mes2) => GlobalHelper.Console.ClearAndPrint(mes1, mes2);

                // старт сервера (на прослушивание подключений)
                output = Task.Run(() => server.Listen());
            }
            else
            {
                Console.WriteLine(protocol + "-сервер не запущен. Проверьте настройки.");
            }

            return output;
        }

        /// <summary>
        /// Возвращает настройки сервера
        /// </summary>
        /// <param name="protocol">Протокол</param>
        /// <returns>Настройки сервера</returns>
        private ServerSettingsModel GetSettings(ConProtocols protocol)
        {
            ServerSettingsModel output = new ServerSettingsModel();

            output.ConProtocol = protocol;
            output.ServerIpAddress = GlobalConfig.GetAppSettingsByKey("serverIpAddress");

            switch (protocol)
            {
                case ConProtocols.Socket:
                    output.ServerPort = GlobalConfig.GetAppSettingsByKey("socketPort");
                    break;

                case ConProtocols.Tcp:
                    output.ServerPort = GlobalConfig.GetAppSettingsByKey("tcpPort");
                    break;

                case ConProtocols.WebSoket:
                    break;
            }

            return output;
        }

        /// <summary>
        /// Производит валидацию настроек сервера
        /// </summary>
        private bool ValidateServerSettings(ServerSettingsModel serverSettings)
        {
            bool output;

            string errMessage;

            output = serverSettings.ValidateIpAddress(out errMessage);
            Console.WriteLine(errMessage);

            output &= serverSettings.ValidatePort(out errMessage);
            Console.WriteLine(errMessage);

            return output;
        }

    }
}
