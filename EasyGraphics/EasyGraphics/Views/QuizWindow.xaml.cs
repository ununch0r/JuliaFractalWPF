using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using EasyGraphics.Constants;
using EasyGraphics.ViewModels;

namespace EasyGraphics.Views
{
    /// <summary>
    /// Interaction logic for QuizWindow.xaml
    /// </summary>
    public partial class QuizWindow : Window
    {
        private readonly Window _parentWindow;
        public QuizWindow(Window parentWindow)
        {
            _parentWindow = parentWindow;
            var quizData = new QuizDataViewModel()
            {
                Questions = StringConstants.Questions
            };
            quizData.CurrentQuestion = quizData.Questions.First();
            DataContext = quizData;
            InitializeComponent();
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            

        }
    }
}
