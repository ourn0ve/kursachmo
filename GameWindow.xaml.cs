using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SnakeGame1
{
    public partial class GameWindow : Window
    {
        private Snake snake;
        private GameField gameField;
        private HighScoreManager highScoreManager = new HighScoreManager();
        private int score;
        private bool isPaused = false;
        private System.Windows.Threading.DispatcherTimer gameTimer;

        private SolidColorBrush SnakeColor { get; set; }

        public GameWindow(bool isHardMode, string snakeColor, string backgroundColor, int fieldSize)
        {
            InitializeComponent();

            // Применяем настройки
            GameCanvas.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(backgroundColor));
            SnakeColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString(snakeColor));

            // Инициализация игрового поля и змейки
            gameField = new GameField(fieldSize, fieldSize, isHardMode);
            snake = new Snake(new Point(fieldSize / 2, fieldSize / 2));

            // Генерация начальной еды
            gameField.Food = gameField.GenerateFood();

            // Отображение рекорда
            int highScore = highScoreManager.GetHighScore(isHardMode ? "Hard" : "Classic");
            HighScoreText.Text = $"High Score: {highScore}";

            // Обработчик нажатия клавиш
            this.KeyDown += GameWindow_KeyDown;

            // Запуск игры
            StartGame();
        }

        private void StartGame()
        {
            gameTimer = new System.Windows.Threading.DispatcherTimer();
            gameTimer.Interval = TimeSpan.FromMilliseconds(150);
            gameTimer.Tick += (sender, args) =>
            {
                if (!isPaused)
                {
                    UpdateGame();
                    DrawGame();
                }
            };
            gameTimer.Start();
        }

        private void UpdateGame()
        {
            snake.Move();

            if (gameField.CheckCollision(snake))
            {
                EndGame();
                return;
            }

            if (gameField.CheckFoodCollision(snake))
            {
                score++;
            }

            ScoreText.Text = $"Score: {score}";
        }

        private void EndGame()
        {
            gameTimer.Stop();
            highScoreManager.SaveHighScore(score, gameField.Obstacles.Any() ? "Hard" : "Classic");
            MessageBox.Show($"Game Over! Your Score: {score}");
            this.Close();
            new MainWindow().Show();
        }

        private void DrawGame()
        {
            GameCanvas.Children.Clear();

            DrawBorder();
            DrawFood();
            DrawSnake();
            DrawObstacles();
        }

        private void DrawBorder()
        {
            var border = new Rectangle
            {
                Width = gameField.Width * 10,
                Height = gameField.Height * 10,
                Stroke = Brushes.Purple,
                StrokeThickness = 2
            };
            GameCanvas.Children.Add(border);
        }

        private void DrawFood()
        {
            var foodEllipse = new Ellipse
            {
                Width = 15,
                Height = 15,
                Fill = Brushes.Green,
                Margin = new Thickness(gameField.Food.Position.X * 10 - 2.5, gameField.Food.Position.Y * 10 - 2.5, 0, 0)
            };

            var scaleAnimation = new DoubleAnimation
            {
                From = 1,
                To = 1.5,
                Duration = TimeSpan.FromSeconds(0.5),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };

            var opacityAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0.5,
                Duration = TimeSpan.FromSeconds(0.5),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };

            foodEllipse.RenderTransform = new ScaleTransform();
            foodEllipse.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
            foodEllipse.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);
            foodEllipse.BeginAnimation(Ellipse.OpacityProperty, opacityAnimation);

            GameCanvas.Children.Add(foodEllipse);
        }

        private void DrawSnake()
        {
            foreach (var bodyPart in snake.Body)
            {
                var snakeRect = new Rectangle
                {
                    Width = 10,
                    Height = 10,
                    Fill = SnakeColor,
                    Margin = new Thickness(bodyPart.X * 10, bodyPart.Y * 10, 0, 0)
                };
                GameCanvas.Children.Add(snakeRect);
            }
        }

        private void DrawObstacles()
        {
            foreach (var obstacle in gameField.Obstacles)
            {
                var obstacleRect = new Rectangle
                {
                    Width = 10,
                    Height = 10,
                    Fill = Brushes.Red,
                    Margin = new Thickness(obstacle.Position.X * 10, obstacle.Position.Y * 10, 0, 0)
                };
                GameCanvas.Children.Add(obstacleRect);
            }
        }

        private void GameWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    snake.SetDirection(Direction.Up);
                    break;
                case Key.Down:
                    snake.SetDirection(Direction.Down);
                    break;
                case Key.Left:
                    snake.SetDirection(Direction.Left);
                    break;
                case Key.Right:
                    snake.SetDirection(Direction.Right);
                    break;
            }
        }




        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            isPaused = !isPaused;
            PauseButton.Content = isPaused ? "Resume" : "Pause";
        }

       private void RestartButton_Click(object sender, RoutedEventArgs e)
{
    // Перезапуск игры
    snake = new Snake(new Point(10, 10));
    gameField.Food = gameField.GenerateFood(); // Убрали аргументы
    score = 0;
    isPaused = false;
    PauseButton.Content = "Pause";
    UpdateGame();
    DrawGame();
}
    }
}