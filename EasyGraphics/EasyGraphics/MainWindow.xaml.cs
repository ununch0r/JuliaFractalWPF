﻿using System.Windows;
using EasyGraphics.Views;

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

        private void ColorSchemeButton_OnClick(object sender, RoutedEventArgs e)
        {
            var colorSchemeWindow = new ColorSchemeWindow();
            colorSchemeWindow.Show();
        }
    }
}
