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
        public List<Subject> subject { get; set; }
        //public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

        //public virtual ICollection<Test> Tests { get; set; } = new List<Test>();

        public virtual ICollection<Subject> IdSubjects { get; set; } = new List<Subject>();

        public static explicit operator TeacherModel(Teacher teacher)
        {
            return new TeacherModel
            {
                Id = teacher.Id,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Login = teacher.Login,
                Password = teacher.Password

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