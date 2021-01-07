using EasyGraphics.Annotations;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EasyGraphics.ViewModels
{
    public class QuestionViewModel : INotifyPropertyChanged
    {

        private int _order;
        private string _question;
        private bool? _isCorrect;
        private string _selectedOption;
        private string _correctAnswer;



        public int Order
        {
            get => _order;
            set
            {
                OnPropertyChanged(nameof(Order));
                _order = value;
            }
        }

        public string Question
        {
            get => _question;
            set
            {
                OnPropertyChanged(nameof(Question));
                _question = value;
            }
        }

        public bool? IsCorrect
        {
            get => _isCorrect;
            set
            {
                OnPropertyChanged(nameof(IsCorrect));
                _isCorrect = value;
            }
        }

        public string SelectedOption
        {
            get => _selectedOption;
            set
            {
                OnPropertyChanged(nameof(SelectedOption));
                _selectedOption = value;
            }
        }

        public ObservableCollection<string> Options { get; set; }

        public string CorrectAnswer
        {
            get => _correctAnswer;
            set
            {
                OnPropertyChanged(nameof(CorrectAnswer));
                _correctAnswer = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
