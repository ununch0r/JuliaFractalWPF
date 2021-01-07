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
        private readonly QuizDataViewModel _quizDataViewModel;
        public QuizWindow(Window parentWindow)
        {
            _parentWindow = parentWindow;

            _quizDataViewModel = new QuizDataViewModel
            {
                Questions = StringConstants.Questions
            };
            _quizDataViewModel.CurrentQuestion = _quizDataViewModel.Questions.First();

            DataContext = _quizDataViewModel;
            InitializeComponent();

            Result.Visibility = Visibility.Collapsed;
            QuestionsGrid.Visibility = Visibility.Visible;
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            _parentWindow.Show();
        }

        private void FirstOption_OnChecked(object sender, RoutedEventArgs e)
        {
            _quizDataViewModel.CurrentQuestion.SelectedOption = _quizDataViewModel.CurrentQuestion.Options[0];
        }

        private void SecondOption_OnChecked(object sender, RoutedEventArgs e)
        {
            _quizDataViewModel.CurrentQuestion.SelectedOption = _quizDataViewModel.CurrentQuestion.Options[1];
        }

        private void ThirdOption_OnChecked(object sender, RoutedEventArgs e)
        {
            _quizDataViewModel.CurrentQuestion.SelectedOption = _quizDataViewModel.CurrentQuestion.Options[2];
        }

        private void SelectAnswer()
        {
            if (FirstOption.IsChecked ?? false)
            {
                _quizDataViewModel.CurrentQuestion.SelectedOption = _quizDataViewModel.CurrentQuestion.Options[0];
            }
            else if(SecondOption.IsChecked ?? false)
            {
                _quizDataViewModel.CurrentQuestion.SelectedOption = _quizDataViewModel.CurrentQuestion.Options[1];
            }
            else if(ThirdOption.IsChecked ?? false)
            {
                _quizDataViewModel.CurrentQuestion.SelectedOption = _quizDataViewModel.CurrentQuestion.Options[2];
            }
        }

        private void Answer_OnClick(object sender, RoutedEventArgs e)
        {
            SelectAnswer();
            _quizDataViewModel.CurrentQuestion.IsCorrect = _quizDataViewModel.CurrentQuestion.SelectedOption ==
                                                           _quizDataViewModel.CurrentQuestion.CorrectAnswer;

            var index = _quizDataViewModel.Questions.IndexOf(_quizDataViewModel.CurrentQuestion);

            if (index == _quizDataViewModel.Questions.Count - 1)
            {
                ShowResult();
            }
            else
            {
                _quizDataViewModel.CurrentQuestion = _quizDataViewModel.Questions[index + 1];
            }
        }

        private void ShowResult()
        {
            GetCertificate.IsEnabled = false;
            double correctAnswers = _quizDataViewModel.Questions.Count(question => question.IsCorrect == true);
            var score = Math.Round((correctAnswers / _quizDataViewModel.Questions.Count) * 100);

            Score.Text = $"Your score: {score}%";

            if (score > 60)
            {
                GetCertificate.IsEnabled = true;
                Greeting.Text = "Congratulations, you have passed the quiz!";
            }
            else
            {
                Greeting.Text = "You have to answer more questions. Try again!";
            }
            Result.Visibility = Visibility.Visible;
            QuestionsGrid.Visibility = Visibility.Collapsed;
        }

        private void GetCertificate_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
