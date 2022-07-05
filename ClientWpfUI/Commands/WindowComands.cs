using System.Windows.Input;

namespace ClientWpfUI.Commands
{
    public class WindowCommands
    {
        public static RoutedCommand Exit { get; set; }

        static WindowCommands()
        {
            Exit = new RoutedCommand("Exit", typeof(MainWindow));
        }
    }
}
