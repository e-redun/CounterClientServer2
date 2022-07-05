using System.Collections.ObjectModel;
using System.IO;
using CounterLib.Models;
using Newtonsoft.Json;

namespace CounterLib.Services
{
    public class NewtonJsonProcessor : IJsonProcessor
    {
        /// <summary>
        /// Сериализирует ответ сервера
        /// </summary>
        /// <returns></returns>
        public string SerializeCounter(ICounter counter)
        {
            //Messenger.MesAsync("SerializeCounter");
            string output = JsonConvert.SerializeObject(counter, Formatting.None);
            
            //Messenger.MesAsync(output);
            return output;
        }

        /// <summary>
        /// Десериализирует ответ сервера
        /// </summary>
        /// <returns></returns>
        public ICounter DeserializeCounter(string message)
        {
            return JsonConvert.DeserializeObject<ClientCounterModel>(message);
        }

        /// <summary>
        /// Считывает настройки сервера из файла
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        /// <param name="serverSettings">Коллекция настроек</param>
        public void ReadSettings(string filePath, ObservableCollection<ServerSettingsModel> serverSettings)
        {
            if (serverSettings != null)
            {
                serverSettings.Clear();

                if (File.Exists(filePath))
                {
                    ObservableCollection<ServerSettingsModel> settings = JsonConvert.DeserializeObject<ObservableCollection<ServerSettingsModel>>(File.ReadAllText(filePath));

                    foreach (var setting in settings)
                    {
                        serverSettings.Add(setting);
                    }
                }
            }
        }


        /// <summary>
        /// Сохраняет коллекцию настроек сервера в файл
        /// </summary>
        /// <param name="filePath">Путь к фалу</param>
        /// <param name="serverSettings">Коллекция настроек</param>
        public void SaveSettings(string filePath, ObservableCollection<ServerSettingsModel> serverSettings)
        {
            using (StreamWriter file = File.CreateText(filePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                serializer.Serialize(file, serverSettings);
            }
        }
    }
}