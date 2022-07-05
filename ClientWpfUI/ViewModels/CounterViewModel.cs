using ClientWpfUI.Services;
using CounterLib.Connections;
using CounterLib.Enums;
using CounterLib.Models;
using CounterLib.Services;
using System.Windows.Input;

namespace ClientWpfUI.ViewModels
{

    public class CounterViewModel : ObservableObject
    {
        #region Команды Start, Stop, Reset
        public ICommand StartCounterCommand { get; set; }
        public ICommand StopCounterCommand { get; set; }
        public ICommand ResetCounterCommand { get; set; }

        #endregion

        /// <summary>
        /// Модель серверного счетчика на клиентской стороне
        /// </summary>
        public ICounter Counter { get; set; } = new ClientCounterModel();

        /// <summary>
        /// Клиент
        /// </summary>
        public BaseClient Client { get; set; }


        private bool _clientConnected;

        public bool ClientConnected
        {
            get { return _clientConnected; }
            set { OnPropertyChanged(ref _clientConnected, value); }
        }


        public CounterViewModel()
        {
            StartCounterCommand = new RelayCommand<string>(StartCounter, CanStartCounter);
            StopCounterCommand = new RelayCommand<string>(StopCounter, CanStopCounter);
            ResetCounterCommand = new RelayCommand<string>(ResetCounter, CanResetCounter);
        }

        public void GetNewClient(BaseClient client)
        {
            Client = client;
        }


        /// <summary>
        /// Отправляет серверу команду Старт счетчика
        /// </summary>
        /// <param name="obj"></param>
        private void StartCounter(string obj)
        {
            SendCommandMessage(CounterCommands.Start.ToString());
        }

        /// <summary>
        /// Отправляет серверу команду Стоп счетчика
        /// </summary>
        /// <param name="obj"></param>
        private void StopCounter(string obj)
        {
            SendCommandMessage(CounterCommands.Stop.ToString());
        }

        /// <summary>
        /// Отправляет серверу команду Сброс счетчика
        /// </summary>
        /// <param name="obj"></param>
        private void ResetCounter(string obj)
        {
            SendCommandMessage(CounterCommands.Reset.ToString());
        }


        private bool CanStartCounter(string arg)
        {
            if (Client == null) return false;

            //TODO Test
            ClientConnected = Client.Connected;

            if (Client.Connected == true &&
                Counter.State == CounterStates.Reset)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CanStopCounter(string arg)
        {
            if (Client == null) return false;

            if (Client.Connected == true &&
                Counter.State != CounterStates.Reset)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CanResetCounter(string arg)
        {
            if (Client == null) return false;

            if (Client.Connected == true &&
                Counter.State != CounterStates.Reset)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Отправляет команду серверу
        /// </summary>
        /// <param name="comMessage">Команда</param>
        private void SendCommandMessage(string comMessage)
        {
            Client.SendMessage(comMessage);
        }


        /// <summary>
        /// Считывает входящее сообщение и обновляет модель счетчика
        /// </summary>
        internal void UpdateCounter()
        {
            ICounter сounter = GlobalHelper.Json.DeserializeCounter(Client.Connector.IncomingMessage);

            Counter.Value = сounter.Value;
            Counter.State = сounter.State;
        }
    }
}
