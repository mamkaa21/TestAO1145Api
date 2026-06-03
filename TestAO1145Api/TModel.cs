namespace TestAO1145Api
{
    public class TModel
    {
        public int Id { get;  set; }
        public string Name { get; set; }
        public int? CountQuestionTest { get; set; }
        public int? IdSubject { get; set; }
        public int? IdTeacher { get; set; }

        public static explicit operator TModel(Test test)
        { 
            return new TModel { CountQuestionTest = test.CountQuestionTest, IdSubject = test.IdSubject, Id = test.Id, IdTeacher = test.IdTeacher, Name = test.Name };
        }
    }
}