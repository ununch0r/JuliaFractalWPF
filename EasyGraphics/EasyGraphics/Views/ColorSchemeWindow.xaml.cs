using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace EasyGraphics.Views
{
    /// <summary>
    /// Interaction logic for ColorSchemeWindow.xaml
    /// </summary>
    public partial class ColorSchemeWindow : Window
    {
        public ColorSchemeWindow()
        {
            InitializeComponent();
        }

        private void UploadButton_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                        "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                        "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                OriginalPhotoImage.Source = new BitmapImage(new Uri(op.FileName));
            }
        }
    }
}
