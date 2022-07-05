using ClientWpfUI.Services;
using CounterLib;
using CounterLib.Enums;
using CounterLib.Models;
using CounterLib.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ClientWpfUI.ViewModels
{
    public class SettingsViewModel : ObservableObject
    {
        /// <summary>
        /// Коллекция настроек сервера
        /// </summary>
        public ObservableCollection<ServerSettingsModel> ServerSettingsList { get; set; } = 
            new ObservableCollection<ServerSettingsModel>();
        

        private ServerSettingsModel _selectedServerSettings;

        /// <summary>
        /// Выбранные настройки сервера
        /// </summary>
        public ServerSettingsModel SelectedServerSettings
        {
            get { return _selectedServerSettings; }
            set { OnPropertyChanged(ref _selectedServerSettings, value); }
        }


        /// <summary>
        /// Настройки сервера
        /// </summary>
        public ServerSettingsModel ServerSettings { get; set; } = new ServerSettingsModel();


        /// <summary>
        /// Помощник соединения
        /// </summary>
        public ConnectionHelper ConnectionHelper { get; set; } = new ConnectionHelper();


        /// <summary>
        /// Команада - Восстановить сохраненные настройки
        /// </summary>
        public ICommand SaveCommand { get; set; }

        /// <summary>
        /// Команда - Применить измененные настройки
        /// </summary>
        public ICommand ApplyCommand { get; set; }

        public SettingsViewModel()
        {
            // список возможных настроек
            string filePath = GlobalConfig.GetAppSettingsByKey("settingsFilePath");

            GlobalHelper.Json.ReadSettings(filePath, ServerSettingsList);


            // выбор последних сохранненых настроек
            SelectedServerSettings = GetStoredSettings();


            // примение к рабочим настройкам
            ServerSettings = (ServerSettingsModel)SelectedServerSettings.Clone();


            // передача рабочих настроек в ConnectionManager
            ConnectionHelper.GetSettings(ServerSettings);


            SaveCommand = new RelayCommand<string>(SaveExecute, CanSaveExecute);

            ApplyCommand = new RelayCommand<string>(ApplyExecute, CanApplyExecute); 
        }


        /// <summary>
        /// Возвращает возможность применения команды Применить
        /// </summary>
        /// <param name="arg"></param>
        /// <returns>True, если команду можно применить</returns>
        private bool CanApplyExecute(string arg)
        {
            bool output;

            // настройки не применены
            output = !ServerSettings.Equals(SelectedServerSettings);

            // настройки валидны
            output &= IsSettingsValid();

            return output;
        }


        /// <summary>
        /// Применяем выюранные настройки к рабочим
        /// </summary>
        /// <param name="obj"></param>
        private void ApplyExecute(string obj)
        {
            ServerSettings = (ServerSettingsModel)SelectedServerSettings.Clone();

            // передача рабочих настроек в ConnectionManager
            ConnectionHelper.GetSettings(ServerSettings);

        }

        /// <summary>
        /// Возвращает возможность применения команды Save
        /// </summary>
        /// <param name="arg"></param>
        /// <returns>True, если команду можно применить</returns>
        private bool CanSaveExecute(string arg)
        {
            bool output;

            // настройки валидны
            output = IsSettingsValid();

            return output;
        }


        private void SaveExecute(string obj)
        {
            // сохранение названия текущего протокола
            Properties.Settings.Default.ConProtocol = ServerSettings.ConProtocol.ToString();
            Properties.Settings.Default.Save();

            // путь к файлу настроек
            string filePath = GlobalConfig.GetAppSettingsByKey("settingsFilePath");

            // сохранение настроек в файл
            GlobalHelper.Json.SaveSettings(filePath, ServerSettingsList);
        }


        private bool IsSettingsValid()
        {
            bool output = true;

            // проверка на валидность выбранных настроек
            output &= string.IsNullOrEmpty(SelectedServerSettings["ServerIpAddress"]);
            output &= string.IsNullOrEmpty(SelectedServerSettings["ServerPort"]);

            return output;
        }


        private ServerSettingsModel GetStoredSettings()
        {

            string strProtocol = Properties.Settings.Default.ConProtocol;

            // протокол соединения
            ConProtocols conProtocol = (ConProtocols)Enum.Parse(typeof(ConProtocols), strProtocol);

            return ServerSettingsList.First(p => p.ConProtocol == conProtocol);
        }
    }
}