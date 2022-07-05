using System.Configuration;

namespace CounterLib
{
    /// <summary>
    /// Класс глобального конфигурирования
    /// </summary>
    public static class GlobalConfig
    {
        //TODO сократить ???
        /// <summary>
        /// Возвращает настройки хранимые в файле App.config по ключу
        /// </summary>
        /// <param name="key">Ключ настройки</param>
        /// <returns>Настройка</returns>
        public static string GetAppSettingsByKey(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}