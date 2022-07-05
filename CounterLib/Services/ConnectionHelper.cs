using CounterLib.Enums;
using CounterLib.Models;
using System;
using System.Net;
using System.Net.Sockets;

namespace CounterLib.Services
{
    public class ConnectionHelper
    {
        /// <summary>
        /// Настройки сервера
        /// </summary>
        private ServerSettingsModel ServerSettings { get; set; }

        public void GetSettings(ServerSettingsModel serverSettings)
        {
            ServerSettings = serverSettings;
        }

        public ConProtocols ConProtocol()
        {
            return ServerSettings.ConProtocol;
        }


        /// <summary>
        /// Возвращате IP сервера
        /// </summary>
        /// <returns>IP сервера</returns>
        public IPAddress ServerIPAddress()
        {
            return IPAddress.Parse(ServerSettings.ServerIpAddress);
        }

        /// <summary>
        /// Возвращате порт сервера
        /// </summary>
        /// <returns>Порт сервера</returns>
        public int Port()
        {
            return int.Parse(ServerSettings.ServerPort);
        }

        /// <summary>
        /// Возврашает сетевую конечную точку
        /// </summary>
        /// <returns>Сетевая конечная точка</returns>
        public IPEndPoint EndPoint()
        {
            return new IPEndPoint(IPAddress.Parse(ServerSettings.ServerIpAddress), Port());
        }

        /// <summary>
        /// Создает Socket-Listener
        /// </summary>
        /// <returns>Socket-Listener</returns>
        public Socket CreateSocketListener()
        {
            Socket socket = null;

            try
            {
                // создание TCP/IP сокета.
                socket = CreateSocket();

                // связывание сокета с локальной точкой, по которой принимаяются данные
                socket.Bind(EndPoint());

                // прослушивание сокета
                socket.Listen(10);
            }
            catch (Exception)
            {
                throw;
            }

            return socket;
        }

        /// <summary>
        /// Создает TcpListener
        /// </summary>
        /// <returns>TcpListener</returns>
        public TcpListener CreateTcpListener()
        {
            return new TcpListener(IPAddress.Any, Port());
        }


        /// <summary>
        /// Создает сокет
        /// </summary>
        /// <returns>Сокет</returns>
        public Socket CreateSocket()
        {
            return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
    }
}
