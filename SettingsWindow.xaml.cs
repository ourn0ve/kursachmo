using System.Windows;
using System.Windows.Controls;

namespace SnakeGame1
{
    public partial class SettingsWindow : Window
    {
        public string SelectedSnakeColor { get; private set; } = "Black";
        public string SelectedBackgroundColor { get; private set; } = "LightGray";
        public int SelectedFieldSize { get; private set; } = 20; // По умолчанию средний размер

        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Сохраняем выбранные настройки
            SelectedSnakeColor = (SnakeColorComboBox.SelectedItem as ComboBoxItem)?.Tag.ToString();
            SelectedBackgroundColor = (BackgroundColorComboBox.SelectedItem as ComboBoxItem)?.Tag.ToString();
            SelectedFieldSize = int.Parse((FieldSizeComboBox.SelectedItem as ComboBoxItem)?.Tag.ToString());

            this.DialogResult = true; // Закрываем окно с результатом "OK"
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false; // Закрываем окно с результатом "Cancel"
        }
    }
}