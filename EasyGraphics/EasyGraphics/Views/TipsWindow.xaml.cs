using System.Collections.Generic;
using System.Linq;
using System.Windows;
using EasyGraphics.ViewModels;

namespace EasyGraphics.Views
{
    public partial class TipsWindow : Window
    {
        private readonly Window _parentWindow;
        public TipsWindow(List<string> pages, string pictureName, Window parent)
        {
            var dataModel = new TipViewModel
            {
                Pages = pages,
                CurrentPage = pages.First(),
                PictureName = pictureName
            };
            _parentWindow = parent;
            DataContext = dataModel;
            InitializeComponent();
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            _parentWindow.Show();
        }
    }
}
