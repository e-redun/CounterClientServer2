using CounterLib.Services;
using System.Windows;

namespace ClientWpfUI
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            GlobalHelper.Initialize();
        }
    }
}
