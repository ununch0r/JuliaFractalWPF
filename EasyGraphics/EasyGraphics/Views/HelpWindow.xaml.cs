using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using EasyGraphics.Annotations;

namespace EasyGraphics.Views
{
    /// <summary>
    /// Interaction logic for HelpWindow.xaml
    /// </summary>
    public partial class HelpWindow : Window, INotifyPropertyChanged
    {
        public string PictureName { get; set; }

        private readonly Window _parentWindow;
        private readonly Window _homeWindow;
        public HelpWindow(string pictureName, Window parent, Window home)
        {
            InitializeComponent();
            DataContext = this;
            PictureName = pictureName;
            _parentWindow = parent;
            _homeWindow = home;
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            _parentWindow.Show();
        }

        private void HomeButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            _homeWindow.Show();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
