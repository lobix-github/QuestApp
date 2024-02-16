using DB.Models;
using System.Collections.Generic;
using System.Linq;

namespace QuestApp.DTO
{
    public class QuestionDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Comment { get; set; }
        public double Wage { get; set; }
        public int Index { get; set; }
        public List<AnswerDTO> Answers { get; set; } = new List<AnswerDTO>();

        public int SelectedAnswerId { get; set; }

        private int selectedAnswerIndex;
        public int SelectedAnswerIndex
        {
            get => selectedAnswerIndex;
            set
            {
                selectedAnswerIndex = value;
                SelectedAnswerId = Answers.Where(x => x.Index == selectedAnswerIndex).Select(x => x.Id).FirstOrDefault();
            }
        }
        public Answer[] SelectedAnswers { get; set; }
    }
}
