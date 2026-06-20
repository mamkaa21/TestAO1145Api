using static System.Net.Mime.MediaTypeNames;

namespace TestAO1145Api
{
    public class TeacherModel
    {
        public int Id { get;  set; }
        public string FirstName { get;  set; }
        public string LastName { get;  set; }
        public string Login { get;  set; }
        public string Password { get; set; }
        public List<SubModel> subject { get; set; }


        public static explicit operator TeacherModel(Teacher teacher)
        {
            var result = new TeacherModel
            {
                Id = teacher.Id,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Login = teacher.Login,
                Password = teacher.Password,
                subject = teacher.IdSubjects.Select(s => (SubModel)s).ToList()
            }; 
            return result;
        }
       
        public static explicit operator Teacher(TeacherModel teacher)
        {
            return new Teacher
            {
                Id = teacher.Id,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Login = teacher.Login,
                Password = teacher.Password,
                IdSubjects = teacher.subject.Select(s => (Subject)s).ToList()
            };

 
                //};
                //var result = new TeacherModel
                //{
                //    Id = teacher.Id,
                //    FirstName = teacher.FirstName,
                //    LastName = teacher.LastName,
                //    Login = teacher.Login,
                //    Password = teacher.Password,
                //};
                //if (teacher.IdSubjects != null)
                //    result.subject = teacher.IdSubjects.ToList(); //спросить как при получении
                //                                                  //препода можно вытянуть и предметы из таблицу кроса

                //    var s = teacher.IdSubjects.Select(s => s.Name);

               
        }
    }
}