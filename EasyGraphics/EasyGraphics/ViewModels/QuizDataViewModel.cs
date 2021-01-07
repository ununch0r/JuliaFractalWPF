using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using EasyGraphics.Annotations;

namespace EasyGraphics.ViewModels
{
    public class QuizDataViewModel : INotifyPropertyChanged
    {
        private QuestionViewModel _currentQuestion;
        public QuestionViewModel CurrentQuestion 
        { 
            get => _currentQuestion;
            set
            {
                _currentQuestion = value;
                OnPropertyChanged(nameof(CurrentQuestion));
            }
        }
        public ObservableCollection<QuestionViewModel> Questions { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
