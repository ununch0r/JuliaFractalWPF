using System.Collections.Generic;

namespace EasyGraphics.ViewModels
{
    public class QuizDataViewModel
    {
        public QuestionViewModel CurrentQuestion { get; set; }
        public List<QuestionViewModel> Questions { get; set; }
    }
}
