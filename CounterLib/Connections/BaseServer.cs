using CounterLib.Models;
using CounterLib.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using static CounterLib.Delegates.Delegates;

namespace CounterLib.Connections
{
    public abstract class BaseServer
    {
        /// <summary>
        /// Счетчик
        /// </summary>
        public ICounter Counter { get; set; }

        /// <summary>
        /// Помощник соединения
        /// </summary>
        public ConnectionHelper ConnectionHelper { get; set; }


        /// <summary>
        /// Клиенты подключенные к серверу
        /// </summary>
        internal List<ConnectedObject> clients = new List<ConnectedObject>();

        public PrintMesDel Print;

        public ClearAndPrintMesDel ClearAndPrint;


        /// <summary>
        /// Подписывает методы сервера на тиканье таймера счетчика
        /// </summary>
        public void SubscibeOnCounter()
        {
            // рассылку сообщения клиентам
            ((CounterModel)Counter).RegisterTimerTickHandler(Broadcast);

            // отображение значения/состояния счетчика в консоли
            ((CounterModel)Counter).RegisterTimerTickHandler(() => ClearAndPrint?.Invoke(Counter.Value, Counter.State));
        }

        /// <summary>
        /// Добавляет соединение (подключенного клиента)
        /// </summary>
        /// <param name="clientObject">Клиент</param>
        protected internal void AddConnection(ConnectedObject clientObject)
        {
            clients.Add(clientObject);
        }

        /// <summary>
        /// Выводит сообщение о запуске сервера
        /// </summary>
        public void PrintStartServerMessage()
        {
            string protocol = ConnectionHelper.ConProtocol().ToString();

            Print?.Invoke(protocol + "-cервер запущен. Ожидание подключений...");
        }

        /// <summary>
        /// Выводит сообщение о подключении клиента
        /// </summary>
        public void PrintConnectedClientMessage(ConnectedObject client)
        {
            ClearAndPrint?.Invoke(client.Id + " подключился");
        }

        /// <summary>
        /// Запускает обслуживания клиентом на сревере входящих сообщений
        /// </summary>
        /// <param name="client"></param>
        public void StartClientOnServerToProcess(ConnectedObject client)
        {
            Thread clientThread = new Thread(new ThreadStart(client.Process));

            clientThread.Start();
        }


        /// <summary>
        /// Удаляет соединение (подключенного клиента)
        /// </summary>
        /// <param name="id">ID клиента</param>
        protected internal void RemoveConnection(string id)
        {
            // получаем по id закрытое подключение
            ConnectedObject client = clients.FirstOrDefault(c => c.Id == id);

            // и удаляем его из списка подключений
            if (client != null)
            {
                clients.Remove(client);
            }
        }

        
        /// <summary>
        /// Транслирует состояние счетчика всем подключенным клиентам
        /// </summary>
        public void Broadcast()
        {
            string message = GlobalHelper.Json.SerializeCounter(Counter);

            foreach (var client in clients)
            {
                client.OutgoingMessage = message;
                client.SendOutgoingMessage();
            }
        }

        /// <summary>
        /// Прослушивает и акцептирует входящие подключения
        /// </summary>
        public abstract void Listen();

        /// <summary>
        /// Отключает всех клиентов, заканчивает работу
        /// </summary>
        public abstract void Disconnect();
    }
}