namespace TestAO1145Api
{
    public class StAModel
    {
        public int Id { get; set; }

        public int? IdStudent { get; set; }

        public int? IdTest { get; set; }

        public DateTime? DateTime { get; set; }

        public int? Mark { get; set; }
        public virtual ICollection<TCrossQ> Testcrossquestions { get; set; } = new List<TCrossQ>();

        public static explicit operator Studentanswer(StAModel model)
        {
            return new Studentanswer
            {
                Id = model.Id,
                IdStudent = model.IdStudent,
                IdTest = model.IdTest,
                DateTime = model.DateTime,
                IdMark  = model.Mark,
                Testcrossquestions = model.Testcrossquestions.Select(s => (Testcrossquestion)s).ToList()

            };
        }

        public static explicit operator StAModel(Studentanswer model)
        {
            return new StAModel
            {
                Id = model.Id,
                IdStudent = model.IdStudent,
                IdTest = model.IdTest,
                DateTime = model.DateTime,
                Mark = model.IdMark,
                Testcrossquestions = model.Testcrossquestions.Select(s => (TCrossQ)s).ToList()

            };
        }
    }
}
