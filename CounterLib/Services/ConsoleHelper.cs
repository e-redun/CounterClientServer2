using System;

namespace CounterLib.Services
{
    public class ConsoleHelper
    {
        /// <summary>
        /// Печатает в консоль сообщение с очисткой экрана
        /// </summary>
        /// <param name="obj1">Сообщение 1</param>
        /// <param name="obj2">Сообщение 2</param>
        public void ClearAndPrint(object obj1, object obj2 = null)
        {
            Console.Clear();

            Console.WriteLine(obj1);

            if (obj2 != null)
            {
                Console.WriteLine(obj2);
            }
        }
    }
}
