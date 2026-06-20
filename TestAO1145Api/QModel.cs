namespace TestAO1145Api
{
    public class QModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int? IdTest { get; set; }
        public int? Type { get; set; }

        public virtual ICollection<QAnswer> Answers { get; set; } = new List<QAnswer>();
        public static explicit operator QModel(Question question)
        {
            return new QModel
            {
                Answers = question.Answers.Select(s => (QAnswer)s).ToList(),
                Id = question.Id,
                Name = question.Name,
                IdTest = question.IdTest,
                Type = question.Type
            };
        }

        public static explicit operator Question(QModel question)
        {
            return new Question
            {
                Answers = question.Answers.Select(s => (Answer)s).ToList(),
                Id = question.Id,
                Name = question.Name,
                IdTest = question.IdTest,
                Type = question.Type
            };
        }
    }
}