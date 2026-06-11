namespace TestAO1145Api
{
    public class TeacherModel
    {
        public int Id { get;  set; }
        public string FirstName { get;  set; }
        public string LastName { get;  set; }
        public string Login { get;  set; }
        public string Password { get; set; }

        public static explicit operator TeacherModel(Teacher teacher)
        {
            return new TeacherModel
            { Id = teacher.Id, 
                FirstName = teacher.FirstName, 
                LastName = teacher.LastName, 
                Login = teacher.Login, 
                Password = teacher.Password 
            };
        }
    }
}