using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
            var fileDialog = new OpenFileDialog
            {
                Title = "Select a picture",
                Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                         "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                         "Portable Network Graphic (*.png)|*.png"
            };

            if (fileDialog.ShowDialog() == true)
            {
                var bitmapImage = new BitmapImage(new Uri(fileDialog.FileName));
                OriginalPhotoImage.Source = bitmapImage;
                ChangedPhotoImage.Source = bitmapImage;
            }
        }

        private void ChangedPhotoImage_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var x = Mouse.GetPosition(ChangedPhotoImage).X;
                var y = Mouse.GetPosition(ChangedPhotoImage).Y;
                var color = PickColor(x, y);
                PixelColorDisplay.Background = new SolidColorBrush(color);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// Picks the color at the position specified.
        /// </summary>
        /// <param name="x">The x coordinate in WPF pixels.</param>
        /// <param name="y">The y coordinate in WPF pixels.</param>
        /// <returns>The image pixel color at x,y position.</returns>
        Color PickColor(double x, double y)
        {
            if (ChangedPhotoImage.Source is BitmapSource bitmapSource)
            {
                x *= bitmapSource.PixelWidth / ChangedPhotoImage.ActualWidth;
                if ((int) x > bitmapSource.PixelWidth - 1)
                {
                    x = bitmapSource.PixelWidth - 1;
                }
                else if (x < 0)
                {
                    x = 0;
                }
                y *= bitmapSource.PixelHeight / ChangedPhotoImage.ActualHeight;

                if ((int) y > bitmapSource.PixelHeight - 1)
                {
                    y = bitmapSource.PixelHeight - 1;
                }
                else if (y < 0)
                {
                    y = 0;
                }

                if (bitmapSource.Format == PixelFormats.Indexed4)
                {
                    byte[] pixels = new byte[1];
                    int stride = (bitmapSource.PixelWidth *
                                  bitmapSource.Format.BitsPerPixel + 3) / 4;
                    bitmapSource.CopyPixels(new Int32Rect((int)x, (int)y, 1, 1),
                                                           pixels, stride, 0);

                    return bitmapSource.Palette.Colors[pixels[0] >> 4];
                }
                else if (bitmapSource.Format == PixelFormats.Indexed8)
                {
                    byte[] pixels = new byte[1];
                    int stride = (bitmapSource.PixelWidth *
                                  bitmapSource.Format.BitsPerPixel + 7) / 8;
                    bitmapSource.CopyPixels(new Int32Rect((int)x, (int)y, 1, 1),
                                                           pixels, stride, 0);

                    return bitmapSource.Palette.Colors[pixels[0]];
                }
                else
                {
                    byte[] pixels = new byte[4];
                    int stride = (bitmapSource.PixelWidth *
                                  bitmapSource.Format.BitsPerPixel + 7) / 8;
                    bitmapSource.CopyPixels(new Int32Rect((int)x, (int)y, 1, 1),
                                                           pixels, stride, 0);

                    return Color.FromArgb(pixels[3], pixels[2], pixels[1], pixels[0]);
                }
            }

            return Colors.White;
        }
    }
}
