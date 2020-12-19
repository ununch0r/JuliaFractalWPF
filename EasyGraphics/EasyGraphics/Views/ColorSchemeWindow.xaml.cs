using EasyGraphics.ColorSchemes.Extensions;
using EasyGraphics.EasyGraphicsColors;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using EasyGraphics.Constants;

namespace EasyGraphics.Views
{
    /// <summary>
    /// Interaction logic for ColorSchemeWindow.xaml
    /// </summary>
    public partial class ColorSchemeWindow : Window
    {
        private Window _parentWindow;
        public ColorSchemeWindow(Window parentWindow)
        {
            _parentWindow = parentWindow;
            InitializeComponent();
        }

        private double _mouseX;
        private double _mouseY;

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
               // BrightnessSlider.Value = BrightnessSlider.Maximum / 2;
            }
        }

        private void ChangedPhotoImage_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var x = Mouse.GetPosition(ChangedPhotoImage).X;
            var y = Mouse.GetPosition(ChangedPhotoImage).Y;

            _mouseX = x;
            _mouseY = y;

            ShowPixelInfo(x,y);
        }

        private void ShowPixelInfo(double x, double y)
        {
            var color = PickColor(x, y);
            PixelColorDisplay.Background = new SolidColorBrush(color);

            var drawingColor = System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);

            var cmyk = ColorSchemeConverter.RgbToCmyk(color.R, color.G, color.B);
            PrintCMYK(cmyk);

            ColorSchemeConverter.ColorToHSV(drawingColor, out var hue, out var saturation, out var value);
            PrintHSV(hue, saturation, value);
          //  if (isInit == false)
          //  {
          //      isInit = true;
            var hueRounded = Math.Round(hue, 1);
            if (hueRounded > 110 && hueRounded < 130)
            {
                BrightnessSlider.Value = value;
            }
        }

        private void PrintHSV(double hue, double saturation, double value)
        {
            HueTextBlock.Text = Math.Round(hue, 1).ToString(CultureInfo.CurrentCulture) + "°";
            SaturationTextBlock.Text = ColorSchemeConverter.RoundValue(saturation,1).ToString(CultureInfo.CurrentCulture) + "%";
            ValuesTextBlock.Text = ColorSchemeConverter.RoundValue(value).ToString(CultureInfo.CurrentCulture) + "%";
        }

        private void PrintRGB(byte red, byte green, byte blue)
        {
            //Info.Text += $"R: {red}\nG: {green}\nB: {blue}\n";
        }

        private void PrintCMYK(double[] values)
        {
            CyanTextBlock.Text = values[0].ToString(CultureInfo.CurrentCulture);
            MagentaTextBlock.Text = values[1].ToString(CultureInfo.CurrentCulture);
            YellowTextBlock.Text = values[2].ToString(CultureInfo.CurrentCulture);
            BlackTextBlock.Text = values[3].ToString(CultureInfo.CurrentCulture);
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


        private void HsvRadioButton_OnChecked(object sender, RoutedEventArgs e)
        {
            CmykGrid.Visibility = Visibility.Collapsed;
            HsvGrid.Visibility = Visibility.Visible;
        }

        private void CmykRadioButton_OnChecked(object sender, RoutedEventArgs e)
        {
            CmykGrid.Visibility = Visibility.Visible;
            HsvGrid.Visibility = Visibility.Collapsed;
        }

        private void BrightnessSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ChangedPhotoImage.Source is BitmapSource bitmapSource)
            {
                var newBitmap = new WriteableBitmap(bitmapSource);

                var pixelData = newBitmap.CopyPixelsWithStride(out var stride);

                for (var i = 0; i < pixelData.GetLength(0); i++)
                {
                    for (var j = 0; j < pixelData.GetLength(1); j++)
                    {
                        var color = System.Drawing.Color.FromArgb(pixelData[i, j].Alpha, pixelData[i, j].Red, pixelData[i, j].Green,
                            pixelData[i, j].Blue);
                        ColorSchemeConverter.ColorToHSV(color, out var hue, out var saturation, out var value);
                        if (hue > 110 && hue < 130 && saturation > 0.10)
                        {
                            //var difference = (e.NewValue - e.OldValue)*2;

                            //var newValue = value + difference;

                            //if (newValue >= 1)
                            //{
                            //    newValue = 1;
                            //}

                            //if (newValue < 0.01)
                            //{
                            //    newValue = 0.01;
                            //}

                            //value = newValue;
                            if (e.NewValue < 0.01)
                            {
                                value = 0.01;
                            }
                            else
                            {
                                value = e.NewValue;
                            }
                        }

                        var newColor = ColorSchemeConverter.ColorFromHSV(hue, saturation, value);
                        pixelData[i, j].Red = newColor.R;
                        pixelData[i, j].Green = newColor.G;
                        pixelData[i, j].Blue = newColor.B;
                    }
                }

                newBitmap.WritePixels(new Int32Rect(0, 0, newBitmap.PixelWidth, newBitmap.PixelHeight), pixelData, stride, 0);

                ChangedPhotoImage.Source = newBitmap;
                ShowPixelInfo(_mouseX, _mouseY);
            }
        }

        private void DownloadButton_OnClick(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Title = "Select a picture",
                Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                         "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                         "Portable Network Graphic (*.png)|*.png"
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                var filePath = saveFileDialog.FileName;

                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)ChangedPhotoImage.Source));
                using var stream = new FileStream(filePath, FileMode.Create);

                encoder.Save(stream);
            }
        }

        private void HomeButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
            _parentWindow.Show();
        }

        private void TipsButton_OnClick(object sender, RoutedEventArgs e)
        {
            var tipsWindow = new TipsWindow(StringConstants.ColorSchemePages, StringConstants.ColorSchemePictureName, this, _parentWindow);

            Hide();
            tipsWindow.Show();
        }
    }
}
