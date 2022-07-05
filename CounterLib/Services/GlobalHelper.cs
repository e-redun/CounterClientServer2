namespace CounterLib.Services
{
    /// <summary>
    /// Глобальный хэлпер
    /// </summary>
    public static class GlobalHelper
    {
        /// <summary>
        /// Валидатор
        /// </summary>
        public static IValidator Validator { get; set; }

        /// <summary>
        /// Процессор json
        /// </summary>
        public static IJsonProcessor Json { get; set; }

        /// <summary>
        /// Помощник вывода на консоль
        /// </summary>
        public static ConsoleHelper Console { get; set; }

        public static void Initialize()
        {
            Validator = new Validator();

            Json = new NewtonJsonProcessor();

            Console = new ConsoleHelper();
        }
    }
}
