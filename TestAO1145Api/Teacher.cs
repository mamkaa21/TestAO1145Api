using System;
using System.Collections.Generic;

namespace TestAO1145Api;

public partial class Teacher
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? IdClass { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual Class? IdClassNavigation { get; set; }

    public virtual ICollection<Test> Tests { get; set; } = new List<Test>();

    public static explicit operator TeaModel(Teacher teacher)
    {
        return new TeaModel
        {
           Id = teacher.Id,
           FirstName = teacher.FirstName,
           LastName = teacher.LastName,
           Login = teacher.Login,
           Password = teacher.Password,
           IdClass = teacher.IdClass,
           Classes = teacher.Classes,
           Tests = teacher.Tests

        };
    }
}

public partial class TeaModel
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? IdClass { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
    public virtual ICollection<Test> Tests { get; set; } = new List<Test>();
}
