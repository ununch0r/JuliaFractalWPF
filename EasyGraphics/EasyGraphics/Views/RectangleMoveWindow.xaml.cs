using EasyGraphics.Annotations;
using EasyGraphics.Constants;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using OxyPlot;
using OxyPlot.Axes;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Threading;

namespace EasyGraphics.Views
{
    /// <summary>
    /// Interaction logic for RectangleMoveWindow.xaml
    /// </summary>
    public partial class RectangleMoveWindow : Window, INotifyPropertyChanged
    {

        private readonly DispatcherTimer _timer = new DispatcherTimer();
        public RectangleMoveWindow()
        {
            DataContext = this;
            InitializeComponent();

            SetUpModel();
            InitializeCoordinates();

            _timer.Tick += TimerOnElapsed;
            _timer.Interval = new TimeSpan(0, 0, 1);
        }

        public RectangleMoveWindow(Window parentWindow)
        {
            DataContext = this;
            _parentWindow = parentWindow;
            InitializeComponent();

            SetUpModel();
            InitializeCoordinates();

            _timer.Tick += TimerOnElapsed;
            _timer.Interval = new TimeSpan(0, 0, 1);
        }

        private void InitializeCoordinates()
        {
            AVertexXCoordinate = 1;
            AVertexYCoordinate = 1;
            BVertexXCoordinate = 1;
            BVertexYCoordinate = 3;
            CVertexXCoordinate = 5;
            CVertexYCoordinate = 3;
            DVertexXCoordinate = 5;
            DVertexYCoordinate = 1;
        }

        private PlotModel _plotModel = new PlotModel();
        private readonly Window _parentWindow;

        public PlotModel PlotModel
        {
            get => _plotModel;
            set
            {
                _plotModel = value;
                OnPropertyChanged(nameof(PlotModel));
            }
        }

        private void SetUpModel()
        {
            PlotModel.Background = OxyColor.FromArgb(255, 129, 159, 120);

            var xAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                AxislineColor = OxyColor.FromRgb(120, 255, 120),
                AxislineThickness = 1,
                Maximum = 20,
                Minimum = 0
            };

            var yAxis = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                AxislineColor = OxyColor.FromRgb(120, 255, 120),
                AxislineThickness = 1,
                Maximum = 20,
                Minimum = 0
            };

            PlotModel.Axes.Add(xAxis);
            PlotModel.Axes.Add(yAxis);

            MovePlot.Model = PlotModel;
        }

        private void CreateQuadrangle()
        {
            PlotModel.Series.Clear();

            var line1 = new OxyPlot.Series.AreaSeries();
            line1.Points.Add(new DataPoint(_aVertexXCoordinate, _aVertexYCoordinate));
            line1.Points.Add(new DataPoint(_bVertexXCoordinate, _bVertexYCoordinate));
            line1.Points.Add(new DataPoint(_cVertexXCoordinate, _cVertexYCoordinate));
            line1.Points.Add(new DataPoint(_dVertexXCoordinate, _dVertexYCoordinate));
            line1.Points.Add(new DataPoint(_aVertexXCoordinate, _aVertexYCoordinate));
            line1.Color = OxyColor.FromRgb(255, 0, 0);

            PlotModel.Series.Add(line1);

            MovePlot.Model = PlotModel;
            MovePlot.InvalidatePlot(true);
        }

        private double _aVertexXCoordinate;

        public double AVertexXCoordinate
        {
            get => _aVertexXCoordinate;
            set
            {
                _aVertexXCoordinate = value;
                ValidatePoints();
                CreateQuadrangle();
                OnPropertyChanged(nameof(AVertexXCoordinate));
            }
        }

        private double _aVertexYCoordinate;

        public double AVertexYCoordinate
        {
            get => _aVertexYCoordinate;
            set
            {
                _aVertexYCoordinate = value;
                ValidatePoints();
                CreateQuadrangle();
                OnPropertyChanged(nameof(AVertexYCoordinate));
            }
        }

        private double _bVertexXCoordinate;

        public double BVertexXCoordinate
        {
            get => _bVertexXCoordinate;
            set
            {
                _bVertexXCoordinate = value;
                ValidatePoints();
                CreateQuadrangle();
                OnPropertyChanged(nameof(BVertexXCoordinate));
            }
        }

        private double _bVertexYCoordinate;

        public double BVertexYCoordinate
        {
            get => _bVertexYCoordinate;
            set
            {
                _bVertexYCoordinate = value;
                ValidatePoints();
                CreateQuadrangle();
                OnPropertyChanged(nameof(BVertexYCoordinate));
            }
        }

        private double _cVertexXCoordinate;

        public double CVertexXCoordinate
        {
            get => _cVertexXCoordinate;
            set
            {
                _cVertexXCoordinate = value;
                ValidatePoints();
                CreateQuadrangle();
                OnPropertyChanged(nameof(CVertexXCoordinate));
            }
        }

        private double _cVertexYCoordinate;

        public double CVertexYCoordinate
        {
            get => _cVertexYCoordinate;
            set
            {
                _cVertexYCoordinate = value;
                ValidatePoints();
                CreateQuadrangle();
                OnPropertyChanged(nameof(CVertexYCoordinate));
            }
        }

        private double _dVertexXCoordinate;

        public double DVertexXCoordinate
        {
            get => _dVertexXCoordinate;
            set
            {
                _dVertexXCoordinate = value;
                ValidatePoints();
                CreateQuadrangle();
                OnPropertyChanged(nameof(DVertexXCoordinate));
            }
        }

        private double _dVertexYCoordinate;

        public double DVertexYCoordinate
        {
            get => _dVertexYCoordinate;
            set
            {
                _dVertexYCoordinate = value;
                ValidatePoints();
                CreateQuadrangle();
                OnPropertyChanged(nameof(DVertexYCoordinate));
            }
        }

        private double _deltaX;

        public double DeltaX
        {
            get => _deltaX;
            set
            {
                _deltaX = value;
                OnPropertyChanged(nameof(DeltaX));
            }
        }

        private double _deltaY;

        public double DeltaY
        {
            get => _deltaY;
            set
            {
                _deltaY = value;
                OnPropertyChanged(nameof(DeltaY));
            }
        }

        private void ValidatePoints()
        {
            var pointA = new Point(AVertexXCoordinate, AVertexYCoordinate);
            var pointB = new Point(BVertexXCoordinate, BVertexYCoordinate);
            var pointC = new Point(CVertexXCoordinate, CVertexYCoordinate);
            var pointD = new Point(DVertexXCoordinate, DVertexYCoordinate);

            if (pointA == pointB || pointA == pointC || pointA == pointD ||
                pointB == pointC || pointB == pointD || pointC == pointD)
            {
                ErrorText = "Wrong points. Figure isn't a quadrangle.";
                Start.IsEnabled = false;
                Move.IsEnabled = false;
                Back.IsEnabled = false;
            }
            else
            {
                var isABCrossingCD = AreCrossing(pointA, pointB, pointC, pointD);
                var isBCCrossingAD = AreCrossing(pointB, pointC, pointA, pointD);

                if (isABCrossingCD || isBCCrossingAD)
                {
                    ErrorText = "Segments intersects, please specify correct points!";
                    Start.IsEnabled = false;
                    Move.IsEnabled = false;
                    Back.IsEnabled = false;
                }
                else
                {
                    ErrorText = "";
                    Start.IsEnabled = true;
                    Move.IsEnabled = true;
                    Back.IsEnabled = true;
                }
            }
        }

        public bool AreCrossing(Point p1, Point p2, Point p3, Point p4)
        {
            var v1 = VectorMultiplication(p4.X - p3.X, p4.Y - p3.Y, p1.X - p3.X, p1.Y - p3.Y);
            var v2 = VectorMultiplication(p4.X - p3.X, p4.Y - p3.Y, p2.X - p3.X, p2.Y - p3.Y);
            var v3 = VectorMultiplication(p2.X - p1.X, p2.Y - p1.Y, p3.X - p1.X, p3.Y - p1.Y);
            var v4 = VectorMultiplication(p2.X - p1.X, p2.Y - p1.Y, p4.X - p1.X, p4.Y - p1.Y);
            if ((v1 * v2) < 0 && (v3 * v4) < 0)
                return true;
            return false;
        }

        private double VectorMultiplication(double ax, double ay, double bx, double by)
        {
            return ax * by - bx * ay;
        }

        private string _errorText;

        public string ErrorText
        {
            get => _errorText;
            set
            {
                _errorText = value;
                OnPropertyChanged(nameof(ErrorText));
            }
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

        private void DownloadButton_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void UploadButton_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private bool _reverseStep = false;

        private void Start_OnClick(object sender, RoutedEventArgs e)
        {
            _timer.Start();
            Start.Visibility = Visibility.Collapsed;
            Stop.Visibility = Visibility.Visible;
        }

        private void TimerOnElapsed(object sender, EventArgs e)
        {
            var currentMatrix = GetCurrentCoordinatesMatrix();
            Matrix<double> result;

            if (_reverseStep)
            {
                result = GetMatrixAfterReverseAffineTransformations(currentMatrix);
            }
            else
            {
                result = GetMatrixAfterAffineTransformations(currentMatrix);
            }
            SetNewCoordinates(result);
            _reverseStep = !_reverseStep;
        }

        private void SetNewCoordinates(Matrix<double> matrix)
        {
            AVertexXCoordinate = matrix.At(0, 0);
            AVertexYCoordinate = matrix.At(0, 1);
            BVertexXCoordinate = matrix.At(1, 0);
            BVertexYCoordinate = matrix.At(1, 1);
            CVertexXCoordinate = matrix.At(2, 0);
            CVertexYCoordinate = matrix.At(2, 1);
            DVertexXCoordinate = matrix.At(3, 0);
            DVertexYCoordinate = matrix.At(3, 1);
        }

        private Point GetNearestToCenterPoint()
        {
            var min = CalculateDistanceToCenter(AVertexXCoordinate, AVertexYCoordinate);
            var point = new Point(AVertexXCoordinate, AVertexYCoordinate);

            var distance = CalculateDistanceToCenter(BVertexXCoordinate, BVertexYCoordinate);
            if (distance < min)
            {
                min = distance;
                point = new Point(BVertexXCoordinate, BVertexYCoordinate);
            }

            distance = CalculateDistanceToCenter(CVertexXCoordinate, CVertexYCoordinate);
            if (distance < min)
            {
                min = distance;
                point = new Point(CVertexXCoordinate, CVertexYCoordinate);
            }

            distance = CalculateDistanceToCenter(DVertexXCoordinate, DVertexYCoordinate);
            if (distance < min)
            {
                min = distance;
                point = new Point(DVertexXCoordinate, DVertexYCoordinate);
            }

            return point;
        }


        private double CalculateDistanceToCenter(double x, double y)
        {
            var distance = Math.Sqrt(x * x + y * y);
            return distance;
        }

        private Matrix<double> GetMatrixAfterAffineTransformations(DenseMatrix currentMatrix)
        {
            var transformationMatrix = GetTransformationMatrix();
            var resultMatrix = currentMatrix.Multiply(transformationMatrix);

            return resultMatrix;
        }

        private Matrix<double> GetMatrixAfterReverseAffineTransformations(DenseMatrix currentMatrix)
        {
            var transformationMatrix = GetReverseTransformationMatrix();
            var resultMatrix = currentMatrix.Multiply(transformationMatrix);

            return resultMatrix;
        }

        private Matrix<double> GetTransformationMatrix()
        {
            var nearestToCenterPoint = GetNearestToCenterPoint();

            var moveToCenterTranslationMatrix = GetCurrentTranslationMatrix(-nearestToCenterPoint.X, -nearestToCenterPoint.Y);
            var dilationMatrix = GetCurrentDilationMatrix(2,2);
            var moveBackTranslationMatrix = GetCurrentTranslationMatrix(nearestToCenterPoint.X, nearestToCenterPoint.Y);
            var translationMatrix = GetCurrentTranslationMatrix(DeltaX, DeltaY);

            var transformationMatrix = moveToCenterTranslationMatrix.Multiply(dilationMatrix)
                .Multiply(moveBackTranslationMatrix).Multiply(translationMatrix);

            return transformationMatrix;
        }

        private Matrix<double> GetReverseTransformationMatrix()
        {
            var nearestToCenterPoint = GetNearestToCenterPoint();

            var moveToCenterTranslationMatrix = GetCurrentTranslationMatrix(-nearestToCenterPoint.X, -nearestToCenterPoint.Y);
            var dilationMatrix = GetCurrentDilationMatrix(0.5, 0.5);
            var moveBackTranslationMatrix = GetCurrentTranslationMatrix(nearestToCenterPoint.X, nearestToCenterPoint.Y);
            var translationMatrix = GetCurrentTranslationMatrix(-DeltaX, -DeltaY);

            var transformationMatrix = moveToCenterTranslationMatrix.Multiply(dilationMatrix)
                .Multiply(moveBackTranslationMatrix).Multiply(translationMatrix);

            return transformationMatrix;
        }

        private double[] GetCoordinatesArray()
        {
            var array = new[]
            {
                AVertexXCoordinate, BVertexXCoordinate, CVertexXCoordinate, DVertexXCoordinate,
                AVertexYCoordinate, BVertexYCoordinate, CVertexYCoordinate, DVertexYCoordinate,
                1,1,1,1
            };

            return array;
        }

        private double[] GetTranslationArray(double x, double y)
        {
            var array = new[]
            {
                1, 0, x,
                0, 1, y,
                0, 0, 1,
            };

            return array;
        }

        private double[] GetDilationArray(double a, double d)
        {
            var array = new[]
            {
                a, 0, 0,
                0, d, 0,
                0, 0, 1,
            };

            return array;
        }

        private DenseMatrix GetCurrentTranslationMatrix(double x, double y)
        {
            var array = GetTranslationArray(x, y);
            var matrix = new DenseMatrix(3, 3, array);

            return matrix;
        }

        private DenseMatrix GetCurrentDilationMatrix(double a, double d)
        {
            var array = GetDilationArray(a,d);
            var matrix = new DenseMatrix(3, 3, array);

            return matrix;
        }

        private DenseMatrix GetCurrentCoordinatesMatrix()
        {
            var array = GetCoordinatesArray();
            var matrix = new DenseMatrix(4,3, array);

            return matrix;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Move_OnClick(object sender, RoutedEventArgs e)
        {
            var currentMatrix = GetCurrentCoordinatesMatrix();
            var result = GetMatrixAfterAffineTransformations(currentMatrix);
            SetNewCoordinates(result);
        }

        private void Back_OnClick(object sender, RoutedEventArgs e)
        {
            var currentMatrix = GetCurrentCoordinatesMatrix();
            var result = GetMatrixAfterReverseAffineTransformations(currentMatrix);
            SetNewCoordinates(result);
        }

        private void Stop_OnClick(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
            Stop.Visibility = Visibility.Collapsed;
            Start.Visibility = Visibility.Visible;
        }
    }
}
