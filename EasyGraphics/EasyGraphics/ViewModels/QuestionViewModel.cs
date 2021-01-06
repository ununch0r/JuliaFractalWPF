using System.Collections.Generic;

namespace EasyGraphics.ViewModels
{
    public class QuestionViewModel
    {
        public int Order { get; set; }
        public string Question { get; set; }
        public bool? IsCorrect { get; set; }
        public string SelectedOption { get; set; }
        public List<string> Options { get; set; }
        public string CorrectAnswer { get; set; }
    }
}
