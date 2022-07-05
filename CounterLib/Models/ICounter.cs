using CounterLib.Enums;

namespace CounterLib.Models
{
    public interface ICounter
    {
        /// <summary>
        /// Состояние счетчика
        /// </summary>
        CounterStates State { get; set; }

        /// <summary>
        /// Значение счетчика
        /// </summary>
        int Value { get; set; }
    }
}