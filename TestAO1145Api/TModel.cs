namespace TestAO1145Api
{
    public class TModel
    {
        public int Id { get;  set; }
        public string Name { get; set; }
        public int? CountQuestionTest { get; set; }
        public int? IdSubject { get; set; }
        public int? IdTeacher { get; set; }

        public string? Teacher { get; set; }
        public string? Subject { get; set; }

        public static explicit operator TModel(Test test)
        { 
             var result = new TModel { CountQuestionTest = test.CountQuestionTest, IdSubject = test.IdSubject, Id = test.Id, IdTeacher = test.IdTeacher, Name = test.Name };
            if (test.IdTeacherNavigation != null)
                result.Teacher = $"{test.IdTeacherNavigation.FirstName} {test.IdTeacherNavigation.LastName}";
            if (test.IdSubjectNavigation != null)
                result.Subject = test.IdSubjectNavigation.Name;
            return result;
        }
    }
}