using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media.Imaging;
using Game_Explorer.Class;

namespace Game_Explorer.Components
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game
    {
        private readonly string _gamePath;

        public Game(GameInfo game)
        {
            InitializeComponent();

            _gamePath = game.ExecutablePath;
            Title.Text = game.Name;
            if (game.LogoPath != null) Image.ImageSource = new BitmapImage(new Uri(game.LogoPath));
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            if (_gamePath == null)
            {
                MessageBox.Show("Game path not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Process.Start(_gamePath);
            }
        }
    }
}