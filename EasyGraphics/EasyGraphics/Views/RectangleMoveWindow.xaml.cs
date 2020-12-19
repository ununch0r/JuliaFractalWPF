using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using EasyGraphics.Constants;

namespace EasyGraphics.Views
{
    /// <summary>
    /// Interaction logic for RectangleMoveWindow.xaml
    /// </summary>
    public partial class RectangleMoveWindow : Window
    {
        private readonly Window _parentWindow;

        public RectangleMoveWindow(Window parentWindow)
        {
            _parentWindow = parentWindow;
            InitializeComponent();
        }

        private void HomeButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
            _parentWindow.Show();
        }

        private void TipsButton_OnClick(object sender, RoutedEventArgs e)
        {
            var tipsWindow = new TipsWindow(StringConstants.RectangleMovePages, StringConstants.RectangleMovePictureName, this, _parentWindow);

            Hide();
            tipsWindow.Show();
        }

        private void DivisionPriceSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            
        }

        private void DownloadButton_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void UploadButton_OnClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
