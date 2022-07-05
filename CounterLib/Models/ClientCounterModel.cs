using CounterLib.Enums;
using CounterLib.Services;

namespace CounterLib.Models
{
    /// <summary>
    /// Модель счетчика на сервере
    /// </summary>
    public class ClientCounterModel : ObservableObject, ICounter
    {
        private CounterStates _state = CounterStates.Reset;

        /// <summary>
        /// Состояние счетчика
        /// </summary>
        public CounterStates State
        {
            get { return _state; }
            set { OnPropertyChanged(ref _state, value); }
        }


        private int _value = 0;

        /// <summary>
        /// Значение четчика
        /// </summary>
        public int Value
        {
            get { return _value; }
            set { OnPropertyChanged(ref _value, value); }
        }
    }
}
