using CounterLib.Models;
using System;
using System.Net.Sockets;

namespace CounterLib.Connections.Sockets
{
    /// <summary>
    /// Socket-клиент на стороне сервера
    /// </summary>
    public class SocketClientObject : SocketConnectedObject
    {
        private BaseServer Server;

        public SocketClientObject(Socket clientSoket, BaseServer server)
        {
            Id = Guid.NewGuid().ToString();

            Socket = clientSoket;

            Server = server;

            Server.AddConnection(this);
        }

        /// <summary>
        /// Обрабытывает входящие сообщения
        /// </summary>
        public override void Process()
        {
            try
            {
                while (true)
                {
                    try
                    {
                        // создание входящего текстового сообщения
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