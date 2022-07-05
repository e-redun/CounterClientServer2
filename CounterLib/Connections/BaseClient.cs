using CounterLib.Services;
using System;
using static CounterLib.Delegates.Delegates;

namespace CounterLib.Connections
{
    public abstract class BaseClient
    {
        /// <summary>
        /// Коннектор
        /// </summary>
        public ConnectedObject Connector { get; set; }

        /// <summary>
        /// Помощник соединения
        /// </summary>
        public ConnectionHelper ConnectionHelper { get; set; }

        public BaseClient(ConnectedObject connector)
        {
            Connector = connector;
        }

        /// <summary>
        /// Cостояние подключения (коннектора) клиента
        /// </summary>
        /// <returns>Cостояние подключения (коннектора) клиента</returns>
        public bool Connected
        {
            get
            {
                return Connector.Connected();
            }
        }


        /// <summary>
        /// Выполняет подключение клиента
        /// </summary>
        public abstract void Connect();

        /// <summary>
        /// Печатает сообщение
        /// </summary>
        public PrintMesDel Print;

        /// <summary>
        /// Отправляе сообщение
        /// </summary>
        /// <param name="outMessage">Сообщение</param>
        public void SendMessage(string outMessage)
        {
            try
            {
                Connector.OutgoingMessage = outMessage;

                // отправка OutgoingMessage
                Connector.SendOutgoingMessage();
            }
            catch (Exception ex)
            {
                Print?.Invoke(ex.Message);

                // разрыв соединения
                Disconnect();
            }
        }


        public void ReceiveMessage(Action SetInMessage)
        {
            while (true)
            {
                try
                {
                    // считывание входящего текстового сообщения в IncomingMessage
                    Connector.SetIncomingMessage();

                    // обратный вызов - установка IncomingMessage в InMessage модели представления 
                    SetInMessage?.Invoke();
                }
                catch (Exception ex)
                {
                    // Print?.Invoke("Подключение прервано!");

                    // разрыв соединения
                    Disconnect();

                    break;
                }
            }
        }


        /// <summary>
        /// Разрывает соеднение со стороны клиента
        /// </summary>
        public void Disconnect()
        {
            Connector.Close();
        }
    }
}