using EasyGraphics.SubWindows;
using System.Windows;

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
            var fractalsWindow = new FractalWindow();
            fractalsWindow.Show();
        }
    }
}
