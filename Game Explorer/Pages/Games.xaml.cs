using System.Windows;
using Game_Explorer.Class;
using Game_Explorer.Components;

namespace Game_Explorer.Pages
{
    /// <summary>
    /// Interaction logic for Games.xaml
    /// </summary>
    public sealed partial class Games
    {
        public Games()
        {
            InitializeComponent();
        }

        private void LoadGames()
        {
            GamesContainer.Children.Clear();

            var installedGames = GameDetector.DetectInstalledGamesWithLogos();
            foreach (var game in installedGames)
            {
                GamesContainer.Children.Add(new Game(game) { Width = 160, Height = 100, Margin = new Thickness(5) });
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadGames();
        }

        private void ReloadGames_Click(object sender, RoutedEventArgs e)
        {
            LoadGames();
        }
    }
}