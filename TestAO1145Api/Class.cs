using System;
using System.Collections.Generic;
using TestAO1145Api;

namespace TestAO1145Api;

public partial class Class
{
    public int Id { get; set; }

    public int? Number { get; set; }

    public int? IdStudent { get; set; }

    public int? IdTeacher { get; set; }

    public virtual Student? IdStudentNavigation { get; set; }

    public virtual Teacher? IdTeacherNavigation { get; set; }

    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();

    public static explicit operator ClModel(Class clas)
    {
        return new ClModel
        {
          Id = clas.Id,
          Number = clas.Number,
          IdStudent = clas.IdStudent,
          Teachers = clas.Teachers

        };
    }
}


public partial class ClModel
{
    public int Id { get; set; }
    public int? Number { get; set; }
    public int? IdStudent { get; set; }
    public int? IdTeacher { get; set; }
    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
}

