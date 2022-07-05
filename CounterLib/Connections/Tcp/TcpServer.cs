using CounterLib.Services;
using System;
using System.Net.Sockets;

namespace CounterLib.Connections.Sockets
{
    public class TcpServer : BaseServer
    {

        private TcpListener tcpListener;

        // прослушивание входящих подключений
        public override void Listen()
        {
            try
            {
                tcpListener = ConnectionHelper.CreateTcpListener();

                tcpListener.Start();

                // сообщение о запуске сервера
                PrintStartServerMessage();

                while (true)
                {
                    TcpClient tcpClient = tcpListener.AcceptTcpClient();

                    TcpClientObject client = new TcpClientObject(tcpClient, this);

                    // сообщение о подключении клиента
                    PrintConnectedClientMessage(client);

                    StartClientOnServerToProcess(client);
                    //// запуск обслуживания входящих сообщений
                    //Thread clientThread = new Thread(new ThreadStart(client.Process));

                    //clientThread.Start();
                }
            }
            catch (Exception ex)
            {
                // отключение всех клиентов и завершение работы
                Disconnect();
            }
        }


        /// <summary>
        /// Отключение всех клиентов
        /// </summary>
        public override void Disconnect()
        {
            //остановка сервера
            tcpListener.Stop(); 

            // отключение всех клиентов
            foreach (var client in clients)
            {
                client.Close(); 
            }

            //завершение процесса
            Environment.Exit(0); 
        }
    }
}