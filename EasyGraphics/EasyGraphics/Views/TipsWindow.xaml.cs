using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using EasyGraphics.ViewModels;

namespace EasyGraphics.Views
{
    public partial class TipsWindow : Window
    {
        private readonly Window _parentWindow;
        private readonly Window _homeWindow;
        private readonly TipViewModel _viewModel;
        public TipsWindow(List<string> pages, string pictureName, Window parent, Window home)
        {
            _viewModel = new TipViewModel
            {
                Pages = pages,
                CurrentPage = pages.First(),
                PictureName = pictureName
            };
            _parentWindow = parent;
            _homeWindow = home;

            DataContext = _viewModel;
            InitializeComponent();
            LeftButton.IsEnabled = false;
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

        private void RightButton_OnClick(object sender, RoutedEventArgs e)
        {
            var index = _viewModel.Pages.IndexOf(_viewModel.CurrentPage);

            if (index == 0)
            {
                LeftButton.IsEnabled = true;
            }

            if (index == _viewModel.Pages.Count - 2)
            {
                RightButton.IsEnabled = false;
            }

            _viewModel.CurrentPage = _viewModel.Pages[index + 1];
        }

        private void LeftButton_OnClick(object sender, RoutedEventArgs e)
        {
            var index = _viewModel.Pages.IndexOf(_viewModel.CurrentPage);

            if (index == 1)
            {
                LeftButton.IsEnabled = false;
            }

            if (index == _viewModel.Pages.Count - 1)
            {
                RightButton.IsEnabled = true;
            }

            _viewModel.CurrentPage = _viewModel.Pages[index - 1];
        }
    }
}
