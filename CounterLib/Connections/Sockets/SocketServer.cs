using CounterLib.Services;
using System;
using System.Net.Sockets;

namespace CounterLib.Connections.Sockets
{
    /// <summary>
    /// Socket-сервер
    /// </summary>
    public class SocketServer : BaseServer
    {
        public override void Listen()
        {
            Socket socket;

            try
            {
                socket = ConnectionHelper.CreateSocketListener();

                // сообщение о запуске сервера
                PrintStartServerMessage();

                while (true)
                {
                    Socket clientSocket = socket.Accept();

                    SocketClientObject client = new SocketClientObject(clientSocket, this);

                    // сообщение о подключении клиента
                    PrintConnectedClientMessage(client);

                    // запуск обслуживания клиентом на сревере входящих сообщений
                    StartClientOnServerToProcess(client);
                }
            }
            catch (Exception ex)
            {
                Disconnect();
            }
        }
        

        public override void Disconnect()
        {
            foreach (var client in clients)
            {
                client.Close(); //отключение клиента
            }

            Environment.Exit(0); //завершение процесса
        }
    }
}