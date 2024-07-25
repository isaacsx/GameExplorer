using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Game_Explorer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            ((Storyboard)TryFindResource("HomeOpen")).Begin();
            ((Storyboard)TryFindResource("GamesClose")).Begin();

            HomeComponent.Visibility = Visibility.Visible;
            GamesComponent.Visibility = Visibility.Hidden;
        }

        private void GamesButton_Click(object sender, RoutedEventArgs e)
        {
            ((Storyboard)TryFindResource("HomeClose")).Begin();
            ((Storyboard)TryFindResource("GamesOpen")).Begin();

            HomeComponent.Visibility = Visibility.Hidden;
            GamesComponent.Visibility = Visibility.Visible;
        }
    }
}