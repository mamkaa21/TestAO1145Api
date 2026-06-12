using static System.Net.Mime.MediaTypeNames;

namespace TestAO1145Api;

public class StModel
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Age { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? IdClass { get; set; }

    public int? Class { get; set; }

    public static explicit operator Student(StModel stud)
    {
        return new Student
        {
            Age = stud.Age,
            FirstName = stud.FirstName,
            Id = stud.Id,
            IdClass = stud.IdClass,
            LastName = stud.LastName,
            Login = stud.Login,
            Password = stud.Password
        };
    }

    public static explicit operator StModel(Student stud)
    {
        var result = new StModel { Id = stud.Id, FirstName = stud.FirstName, LastName = stud.LastName, Login = stud.Login, Password = stud.Password, Age = stud.Age, IdClass = stud.IdClass };
        if (stud.IdClassNavigation != null)
            result.Class = stud.IdClassNavigation.Number;
        return result;
    }
}