using CounterLib.Enums;
using System;
using System.Threading;

namespace CounterLib.Models
{
    /// <summary>
    /// Модель счетчика на сервере
    /// </summary>
    public class CounterModel : ClientCounterModel
    {
        // Таймер
        private Timer timer;

        [NonSerialized]
        public Action TimerTick;


        public CounterModel()
        {
            // инициализация таймера с интервалом 1000 мс = 1 с
            timer = new Timer(TimerCallback, null, 0, 1000);
        }

        /// <summary>
        /// Обратный вызов таймера
        /// </summary>
        /// <param name="o"></param>
        private void TimerCallback(Object o)
        {
            if (State == CounterStates.Running)
            {
                Value++;
            }

            TimerTick?.Invoke();
        }

        /// <summary>
        /// Подписывает делегат-обработчик на тик таймера
        /// </summary>
        /// <param name="handler"></param>
        public void RegisterTimerTickHandler(Action handler)
        {
            TimerTick += handler;
        }

        /// <summary>
        /// Дает команду счетчику
        /// </summary>
        /// <param name="stringCommand">Команда</param>
        public void CommandByText(string stringCommand)
        {
            bool succesfull = Enum.TryParse(stringCommand, out CounterCommands command);

            if (succesfull)
            {

                switch (command)
                {
                    case CounterCommands.Start:
                        Start();
                        break;

                    case CounterCommands.Stop:
                        Stop();
                        break;

                    case CounterCommands.Reset:
                        Reset();
                        break;
                }
            }
        }

        /// <summary>
        /// Переводит счетчик в рабочее состояние
        /// </summary>
        private void Start()
        {
            if (State == CounterStates.Reset)
            {
                State = CounterStates.Running;
            }
        }

        /// <summary>
        /// Останавливает счетчик
        /// </summary>
        private void Stop()
        {
            switch (State)
            {
                case CounterStates.Running:
                    State = CounterStates.Stopped;
                    break;

                case CounterStates.Stopped:
                    State = CounterStates.Running;
                    break;
            }
        }

        /// <summary>
        /// Перезапускает (обнуляет и останавливает) счетчик
        /// </summary>
        private void Reset()
        {
            if (State != CounterStates.Reset)
            {
                Value = 0;
                State = CounterStates.Reset;
            }
        }
    }
}
