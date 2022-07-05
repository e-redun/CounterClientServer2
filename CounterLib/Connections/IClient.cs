using System;

namespace CounterLib.Connections
{
    public interface IClient
    {
        ConnectedObject Connector { get; set; }

        void Connect();

        bool Connected();

        void SendMessage(string outMessage);

        /// <summary>
        /// Получает входящие сообщения
        /// </summary>
        /// <param name="SetInMessage">Обратный вызов IncomingMessage => MainWindowViewModel.InMessage</param>
        void ReceiveMessage(Action SetInMessage);

        void Disconnect();


        //string IncomingMessage { get; set; }
    }
}