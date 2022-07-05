using ClientWpfUI.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace ClientWpfUI
{

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel mainVM = new MainWindowViewModel();

        public MainWindow()
        {
            InitializeComponent();

           // tcMain.SelectedIndex = 1;

            DataContext = mainVM;
        }

        /// <summary>
        /// Закрывает главное окно MainWindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainVM.Disconnect();
        }
    }
}