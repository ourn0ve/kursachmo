using System.Windows;

namespace SnakeGame1
{
    public partial class MainWindow : Window
    {
        public string SnakeColor { get; set; } = "Black";
        public string BackgroundColor { get; set; } = "LightGray";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ClassicModeButton_Click(object sender, RoutedEventArgs e)
        {
            StartGame(false); // Классический режим
        }

        private void HardModeButton_Click(object sender, RoutedEventArgs e)
        {
            StartGame(true); // Хард-режим
        }


        private void StartGame(bool isHardMode)
        {
            var settingsWindow = new SettingsWindow();
            if (settingsWindow.ShowDialog() == true)
            {
                // Сохраняем выбранные настройки
                SnakeColor = settingsWindow.SelectedSnakeColor;
                BackgroundColor = settingsWindow.SelectedBackgroundColor;
                int fieldSize = settingsWindow.SelectedFieldSize;

                // Запускаем игру с выбранными настройками
                var gameWindow = new GameWindow(isHardMode, SnakeColor, BackgroundColor, fieldSize);
                gameWindow.Show();
                this.Close();
            }
        }
    }
}