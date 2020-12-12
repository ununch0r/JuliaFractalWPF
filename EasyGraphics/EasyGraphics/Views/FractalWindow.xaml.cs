using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using EasyGraphics.Annotations;
using EasyGraphics.EasyGraphicsColors;
using EasyGraphics.Fractals.FractalStrategies;
using EasyGraphics.Fractals.Interfaces;
using Microsoft.Win32;

namespace EasyGraphics.Views
{
    /// <summary>
    /// Interaction logic for FractalWindow.xaml
    /// </summary>
    public partial class FractalWindow : Window, INotifyPropertyChanged
    {
        private static readonly int[] JetColourMap = ColorMaps.GetColourMap("jet");
        private static readonly int[] GistSternColourMap = ColorMaps.GetColourMap("gist_stern");
        private static readonly int[] OceanColourMap = ColorMaps.GetColourMap("ocean");
        private static readonly int[] RainbowColourMap = ColorMaps.GetColourMap("rainbow");
        private static readonly int[] GnuPlotColourMap = ColorMaps.GetColourMap("gnuplot");
        private static readonly int[] AfmHotColourMap = ColorMaps.GetColourMap("afmhot");
        private static readonly int[] GistHeatColourMap = ColorMaps.GetColourMap("gist_heat");
        private readonly IFractal _juliaSet = new JuliaSet();
        private int _fractalImageWidth;
        private int _fractalImageHeight;
        private WriteableBitmap _writeableBitmap;
        private int _maxIterations;
        private int _zoomLevel;
        private Point _bottomLeft;
        private Point _topRight;
        private bool _initDone;
        private int[] _selectedColorMap;

        public static List<Tuple<string, int[]>> AvailableColorMaps => new List<Tuple<string, int[]>>
        {
            Tuple.Create("Jet", JetColourMap),
            Tuple.Create("GistStern", GistSternColourMap),
            Tuple.Create("Ocean", OceanColourMap),
            Tuple.Create("Rainbow", RainbowColourMap),
            Tuple.Create("GnuPlot", GnuPlotColourMap),
            Tuple.Create("AfmHot", AfmHotColourMap),
            Tuple.Create("GistHeat", GistHeatColourMap)
        };

        public FractalWindow()
        {
            InitializeComponent();
            ContentRendered += (_, __) =>
            {
                BottomLeft = new Point(-4d, -4d);
                TopRight = new Point(4d, 4d);

                AdjustAspectRatio();

                MaxIterations = 1;
                ZoomLevel = 1;
                SelectedColourMap = AvailableColorMaps[0].Item2;

                _initDone = true;
                DrawRandom();
            };

            ChangeColorBox.ItemsSource = AvailableColorMaps;
            SizeChanged += FractalWindow_SizeChanged;

            var lastMousePt = new Point();
            var panningInProgress = false;

            MaxIterationsSlider.ValueChanged += (_, __) =>
            {
                MaxIterations = Convert.ToInt32(MaxIterationsSlider.Value);
                Render();
            };

            MouseDown += (_, args) =>
            {
                if ((Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
                {
                    var mousePt = Mouse.GetPosition(FractalImage);
                    var regionWidth = TopRight.X - BottomLeft.X;
                    var regionHeight = TopRight.Y - BottomLeft.Y;
                    var regionCentreX = BottomLeft.X + regionWidth / 2;
                    var regionCentreY = BottomLeft.Y + regionHeight / 2;
                    var regionMouseX = mousePt.X * regionWidth / _fractalImageWidth + BottomLeft.X;
                    var regionMouseY = mousePt.Y * regionHeight / _fractalImageHeight + BottomLeft.Y;
                    var dx = regionMouseX - regionCentreX;
                    var dy = regionMouseY - regionCentreY;
                    BottomLeft = new Point(BottomLeft.X + dx, BottomLeft.Y + dy);
                    TopRight = new Point(TopRight.X + dx, TopRight.Y + dy);
                    Render();
                    return;
                }

                lastMousePt = Mouse.GetPosition(FractalImage);
                panningInProgress = true;
            };

            MouseMove += (_, __) =>
            {
                if (!panningInProgress) return;
                var currentMousePt = Mouse.GetPosition(FractalImage);
                var mouseDx = currentMousePt.X - lastMousePt.X;
                var mouseDy = currentMousePt.Y - lastMousePt.Y;
                var regionWidth = TopRight.X - BottomLeft.X;
                var regionHeight = TopRight.Y - BottomLeft.Y;
                var regionDx = mouseDx / _fractalImageWidth * regionWidth;
                var regionDy = mouseDy / _fractalImageHeight * regionHeight;
                BottomLeft = new Point(BottomLeft.X - regionDx, BottomLeft.Y - regionDy);
                TopRight = new Point(TopRight.X - regionDx, TopRight.Y - regionDy);
                Render();
                lastMousePt = currentMousePt;
            };

            MouseUp += (_, __) =>
            {
                panningInProgress = false;
            };

            MouseLeave += (_, __) =>
            {
                panningInProgress = false;
            };
        }

        private void DrawRandom()
        {
            var values = _juliaSet.CreatePixelArray(
                new Complex(BottomLeft.X, BottomLeft.Y),
                new Complex(TopRight.X, TopRight.Y),
                _fractalImageWidth,
                _fractalImageHeight,
                MaxIterations);


            for (int i = 0; i < values.Length; i++)
            {
                values[i] = (ushort)(new Random().Next(0,10));
            }

            var pixels = ValuesToPixels(values, _selectedColorMap);
            var sourceRect = new Int32Rect(0, 0, _fractalImageWidth, _fractalImageHeight);
            _writeableBitmap.WritePixels(sourceRect, pixels, _writeableBitmap.BackBufferStride, 0);
        }

        private void FractalWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            _fractalImageWidth = (int)Math.Floor(FractalImageWrapper.ActualWidth);
            _fractalImageHeight = (int)Math.Floor(FractalImageWrapper.ActualHeight);
            _writeableBitmap = new WriteableBitmap(
                _fractalImageWidth,
                _fractalImageHeight,
                96,
                96,
                PixelFormats.Bgr32,
                null);

            FractalImage.Source = _writeableBitmap;
            // Render();
        }


        private void AdjustAspectRatio()
        {
            if (_fractalImageWidth > _fractalImageHeight)
            {
                var regionWidth = TopRight.X - BottomLeft.X;
                var newRegionWidthDiff = _fractalImageWidth * regionWidth / _fractalImageHeight - regionWidth;
                var halfNewRegionWidthDiff = newRegionWidthDiff / 2;
                BottomLeft = new Point(BottomLeft.X - halfNewRegionWidthDiff, BottomLeft.Y);
                TopRight = new Point(TopRight.X + halfNewRegionWidthDiff, TopRight.Y);
            }
            else if (_fractalImageHeight > _fractalImageWidth)
            {
                var regionHeight = TopRight.Y - BottomLeft.Y;
                var newRegionHeightDiff = _fractalImageHeight * regionHeight / _fractalImageWidth - regionHeight;
                var halfNewRegionHeightDiff = newRegionHeightDiff / 2;
                BottomLeft = new Point(BottomLeft.X, BottomLeft.Y - halfNewRegionHeightDiff);
                TopRight = new Point(TopRight.X, TopRight.Y + halfNewRegionHeightDiff);
            }

            _writeableBitmap = new WriteableBitmap(
                _fractalImageWidth,
                _fractalImageHeight,
                96,
                96,
                PixelFormats.Bgr32,
                null);

            FractalImage.Source = _writeableBitmap;
        }

        public int[] SelectedColourMap
        {
            get => _selectedColorMap;
            set
            {
                _selectedColorMap = value;
                OnPropertyChanged();
            }
        }

        private void Render()
        {
            if (!_initDone) return;

            var values = _juliaSet.CreatePixelArray(
                new Complex(BottomLeft.X, BottomLeft.Y),
                new Complex(TopRight.X, TopRight.Y),
                _fractalImageWidth,
                _fractalImageHeight,
                MaxIterations);


            var pixels = ValuesToPixels(values, _selectedColorMap);
            var sourceRect = new Int32Rect(0, 0, _fractalImageWidth, _fractalImageHeight);
            _writeableBitmap.WritePixels(sourceRect, pixels, _writeableBitmap.BackBufferStride, 0);
        }

        private static int[] ValuesToPixels(IReadOnlyList<ushort> values, IReadOnlyList<int> colourMap)
        {
            var lastIndex = colourMap.Count - 1;

            var vmin = double.MaxValue;
            var vmax = double.MinValue;

            foreach (var value in values)
            {
                if (value < vmin) vmin = value;
                if (value > vmax) vmax = value;
            }

            var divisor = vmax - vmin;

            var cs = new int[values.Count];
            Parallel.For(0, values.Count, i =>
            {
                var p = values[i];
                var v1 = (p - vmin) / divisor;
                var v2 = double.IsNaN(v1) ? 0d : v1;
                var c = colourMap[(int)Math.Floor(v2 * lastIndex)];
                cs[i] = c;
            });

            return cs;
        }

        public Point BottomLeft
        {
            get { return _bottomLeft; }
            set
            {
                _bottomLeft = value;
                OnPropertyChanged();
            }
        }

        public Point TopRight
        {
            get { return _topRight; }
            set
            {
                _topRight = value;
                OnPropertyChanged();
            }
        }

        public int MaxIterations
        {
            get { return _maxIterations; }
            set
            {
                _maxIterations = value;
                OnPropertyChanged(nameof(MaxIterations));
            }
        }

        public int ZoomLevel
        {
            get { return _zoomLevel; }
            set
            {
                _zoomLevel = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void DrawButton_OnClick(object sender, RoutedEventArgs e)
        {
            Render();
        }

        private void ZoomInButton_OnClick(object sender, RoutedEventArgs e)
        {
            var diff = 1;

            foreach (var idx in Enumerable.Range(0, Math.Abs(diff)))
            {
                var w = TopRight.X - BottomLeft.X;
                var h = TopRight.Y - BottomLeft.Y;
                var dw = w / 4;
                var dh = h / 4;
                BottomLeft = new Point(BottomLeft.X + dw, BottomLeft.Y + dh);
                TopRight = new Point(TopRight.X - dw, TopRight.Y - dh);
            }

            Render();
        }

        private void ZoomOutButton_OnClick(object sender, RoutedEventArgs e)
        {
            var diff = -1;

            foreach (var idx in Enumerable.Range(0, Math.Abs(diff)))
            {
                var w = TopRight.X - BottomLeft.X;
                var h = TopRight.Y - BottomLeft.Y;
                var dw = w / 2;
                var dh = h / 2;
                BottomLeft = new Point(BottomLeft.X - dw, BottomLeft.Y - dh);
                TopRight = new Point(TopRight.X + dw, TopRight.Y + dh);
            }

            Render();
        }

        private void ChangeColorBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedColorMap = AvailableColorMaps[ChangeColorBox.SelectedIndex].Item2;
            DrawRandom();
        }

        private void DownloadButton_OnClick(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                var filePath = saveFileDialog.FileName;

                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)FractalImage.Source));
                using var stream = new FileStream(filePath, FileMode.Create);

                encoder.Save(stream);
            }
        }
    }
}
