using System.Windows;
using EasyGraphics.Views;

namespace EasyGraphics
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void FractalsButton_OnClick(object sender, RoutedEventArgs e)
        {
            var fractalsWindow = new FractalWindow(this);
            this.Hide();
            fractalsWindow.Show();
        }

        private void ColorSchemeButton_OnClick(object sender, RoutedEventArgs e)
        {
            var colorSchemeWindow = new ColorSchemeWindow(this);
            this.Hide();
            colorSchemeWindow.Show();
        }

        private void RectangleMoveButton_OnClick(object sender, RoutedEventArgs e)
        {
            var rectangleMoveWindow = new RectangleMoveWindow(this);
            this.Hide();
            rectangleMoveWindow.Show();
        }

        private void QuizButton_OnClick(object sender, RoutedEventArgs e)
        {
            var rectangleMoveWindow = new QuizWindow(this);
            this.Hide();
            rectangleMoveWindow.Show();
        }
    }
}
