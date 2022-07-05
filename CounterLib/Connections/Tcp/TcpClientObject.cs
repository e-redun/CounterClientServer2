using CounterLib.Connections.Tcp;
using CounterLib.Models;
using System;
using System.Net.Sockets;

namespace CounterLib.Connections.Sockets
{
    /// <summary>
    /// Tcp-клиент на стороне сервера
    /// </summary>
    public class TcpClientObject : TcpConnectedObject
    {
        private BaseServer Server;


        public TcpClientObject(TcpClient tcpClient, BaseServer server)
        {
            Id = Guid.NewGuid().ToString();

            TcpClient = tcpClient;

            Server = server;

            Server.AddConnection(this);
        }

        /// <summary>
        /// Обрабатывает входящие сообщения
        /// </summary>
        public override void Process()
        {
            try
            {
                Stream = TcpClient.GetStream();

                while (true)
                {
                    try
                    {
                        // создание входящего текстового сообщения IncomingMessage
                        SetIncomingMessage();

                        // передача счетчику команды от клиента
                        ((CounterModel)Server.Counter).CommandByText(IncomingMessage);

                        // вывод сообщения
                        Server.ClearAndPrint("Команда счетчику = " + IncomingMessage);

                        // трансляция клиентам состояния счетчика
                        Server.Broadcast();
                    }
                    catch
                    {
                        // вывод сообщения
                        Server.ClearAndPrint(Id + " разорвал соединение");
                        
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                // Server.ClearAndPrint(ex.Message);
            }
            finally
            {
                Server.RemoveConnection(Id);

                Close();
            }
        }
    }
}