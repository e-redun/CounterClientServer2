using ClientWpfUI.Services;
using CounterLib.Connections;
using CounterLib.Enums;
using CounterLib.Factories;
using CounterLib.Services;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace ClientWpfUI.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        #region Свойства

        /// <summary>
        /// Клиент
        /// </summary>
        BaseClient Client { get; set; }

        /// <summary>
        /// Поток принятия/обработки входящих сообщений
        /// </summary>
        Thread receiveThread;

        /// <summary>
        /// Модель представления Настроек
        /// </summary>
        public SettingsViewModel SettingsVM { get; set; } = new SettingsViewModel();

        /// <summary>
        /// Модель представления Счетчика
        /// </summary>
        public CounterViewModel CounterVM { get; set; } = new CounterViewModel();

        /// <summary>
        /// Команда Подключить
        /// </summary>
        public ICommand ConnectCommand { get; set; }

        /// <summary>
        /// Тестовая команда
        /// </summary>
        public ICommand TestCommand { get; set; }

        private bool _clientConnected;

        /// <summary>
        /// Флаг подключения клиента
        /// </summary>
        public bool ClientConnected
        {
            get { return _clientConnected; }
            set { OnPropertyChanged(ref _clientConnected, value); }
        }


        private ConProtocols? _conProtocol;

        /// <summary>
        /// Протокол подключения
        /// </summary>
        public ConProtocols? ConProtocol
        {
            get { return _conProtocol; }
            set { OnPropertyChanged(ref _conProtocol, value); }
        }

        #endregion

        #region Конструктор

        public MainWindowViewModel()
        {
            ConnectCommand = new RelayCommand<string>(ConnectDisconnect, CanConnectDisconnect);

            TestCommand = new RelayCommand<string>(Test);
        }

        #endregion

        #region Методы

        private void Test(string obj)
        {
            if (Client == null) return;

            MessageBox.Show(Client.Connected.ToString());
        }

        private bool CanConnectDisconnect(string arg)
        {
            bool output = true;

            if (Client == null || Client.Connected == false)
            {
                ClientConnected = false;
                ConProtocol = null;
            }
            else
            {
                ClientConnected = true;
                ConProtocol = Client.ConnectionHelper.ConProtocol();
            }

            return output;
        }

        /// <summary>
        /// Подключает/отключает клиента
        /// </summary>
        /// <param name="obj"></param>
        private void ConnectDisconnect(string obj)
        {
            if (Client == null || Client.Connected == false)
            {
                Connect();
            }
            else
            {
                Disconnect();
            }
        }

        /// <summary>
        /// Подключает клиента
        /// </summary>
        private void Connect()
        {
            // инициализация клиента
            Client = Factory.CreateClient(SettingsVM.ServerSettings.ConProtocol);
            Client.ConnectionHelper = SettingsVM.ConnectionHelper;
            Client.Print += (mes) => MessageBox.Show(mes);

            // подключение клиента к серверу
            Client.Connect();

            // передача клиента в CounterVM
            CounterVM.GetNewClient(Client);

            if (Client.Connected)
            {
                // запуск получения сообщений сервера
                receiveThread = new Thread(() => Client.ReceiveMessage(CounterVM.UpdateCounter));
                receiveThread.Start();
            }
        }


        /// <summary>
        /// Отключает клиента
        /// </summary>
        internal void Disconnect()
        {
            if (Client != null)
            {
                Client.Disconnect();
            }

            if (receiveThread != null)
                if (receiveThread.IsAlive)
                {
                    receiveThread.Abort();
                    receiveThread = null;
                }
        }

        #endregion
    }
}