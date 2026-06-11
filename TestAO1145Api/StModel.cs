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
        return new StModel
        {
            Age = stud.Age,
            FirstName = stud.FirstName,
            Id = stud.Id,
            IdClass = stud.IdClass,
            LastName = stud.LastName,
            Login = stud.Login,
            Password = stud.Password,
            Class = stud.IdClassNavigation?.Number
        };
    }
}