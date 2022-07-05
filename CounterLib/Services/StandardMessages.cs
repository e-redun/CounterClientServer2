namespace CounterLib.Services
{
    /// <summary>
    /// Набор стандартных сообщений
    /// </summary>
    public struct StandardMessages
    {
        /// <summary>
        /// Состояния счетчика
        /// </summary>
        public struct CounterStates
        {
            public static string Stopped { get; } = "остановлен";
            public static string Running { get; } = "работает";
            public static string Reset { get; } = "обнулен";
        }


        /// <summary>
        /// При валидации
        /// </summary>
        public struct Validation
        {
            public static string NotValidIp { get; } = "Некорректный IP.";

            public static string NotValidPort { get; } = "Некорректный номер порта";
        }
    }
}
