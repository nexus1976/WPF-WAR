using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WAR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel ViewModel { get { return this.DataContext as MainWindowViewModel; } }
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
        }

        private void btnNewGame_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.NewGame();
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Play();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnRules_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(ViewModel.GameRules, "How To Play WAR", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
