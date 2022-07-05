using CounterLib.Enums;
using CounterLib.Services;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ClientWpfUI.Converters
{
    /// <summary>
    /// Преобразует Дату/время
    /// </summary>
    public class CounterStateToRusConverter : IValueConverter
    {
        /// <summary>
        /// Преобразует состояние клиента в русский текст
        /// </summary>
        /// <param name="value">Состояние клиента</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((CounterStates)value)
            {
                case CounterStates.Running:
                    return StandardMessages.CounterStates.Running;

                case CounterStates.Stopped:
                    return StandardMessages.CounterStates.Stopped;

                case CounterStates.Reset:
                    return StandardMessages.CounterStates.Reset;

                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
