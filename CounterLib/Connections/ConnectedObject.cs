using System.Text;

namespace CounterLib.Connections
{
    /// <summary>
    /// Класс-оболочка для клиентского соединения
    /// </summary>
    public abstract class ConnectedObject
    {
        #region Свойства

        /// <summary>
        /// ID клиента
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Размер буфера
        /// </summary>
        internal int BufferSize { get; set; } = 256;

        /// <summary>
        /// StringBuilder сообщений
        /// </summary>
        internal StringBuilder MessageBuilder { get; set; } = new StringBuilder();

        // Полученная текстовая строка данных
        public string IncomingMessage { get; set; } = "";

        // Текстовая строка данных на отправку
        public string OutgoingMessage { get; set; } = "";

        /// <summary>
        /// Терминатор сообщения
        /// </summary>
        public string MessageTerminator { get; set; } = "<END>";

        #endregion


        #region Методы

        /// <summary>
        /// Возвращает состояние подключения
        /// </summary>
        /// <returns></returns>
        public abstract bool Connected();


        /// <summary>
        /// Отправляет исходящее сообщение на подклюенный сокет
        /// </summary>
        public abstract void SendOutgoingMessage();

        /// <summary>
        /// Считывает 
        /// </summary>
        public abstract void SetIncomingMessage();

        /// <summary>
        /// Закрывает соединение
        /// </summary>
        public abstract void Close();

        /// <summary>
        /// Обрабытывает входящие сообщения
        /// </summary>
        public abstract void Process();

        #endregion
    }
}