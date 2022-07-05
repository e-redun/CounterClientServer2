using CounterLib.Models;
using System.Collections.ObjectModel;

namespace CounterLib.Services
{
    public interface IJsonProcessor
    {
        /// <summary>
        /// Сериализирует ответ сервера
        /// </summary>
        /// <returns></returns>
        string SerializeCounter(ICounter counter);

        /// <summary>
        /// Десериализирует ответ сервера
        /// </summary>
        /// <returns></returns>
        ICounter DeserializeCounter(string message);

        /// <summary>
        /// Считывает настройки сервера из файла
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        /// <param name="serverSettings">Коллекция настроек</param>
        void ReadSettings(string filePath, ObservableCollection<ServerSettingsModel> serverSettings);

        /// <summary>
        /// Сохраняет коллекцию настроек сервера в файл
        /// </summary>
        /// <param name="filePath">Путь к фалу</param>
        /// <param name="serverSettings">Коллекция настроек</param>

        void SaveSettings(string filePath, ObservableCollection<ServerSettingsModel> serverSettings);
    }
}