namespace TestAO1145Api
{
    public class QAnswer
    {
        public int Id { get; set; }
        public string? Text { get; set; }

        public int? IdQuestion { get; set; }

        public bool? RightAnswer { get; set; }

        public static explicit operator Answer(QAnswer answer)
        {
            return new Answer { Id = answer.Id, Text = answer.Text, IdQuestion = answer.IdQuestion, RightAnswer = answer.RightAnswer };
        }

        public static explicit operator QAnswer(Answer answer)
        {
            return new QAnswer { Id = answer.Id, Text = answer.Text, IdQuestion = answer.IdQuestion, RightAnswer = answer.RightAnswer };
        }
    }
}