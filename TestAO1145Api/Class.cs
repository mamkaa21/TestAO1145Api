using System;
using System.Collections.Generic;

namespace TestAO1145Api;

public partial class Class
{
    public int Id { get; set; }

    public int? Number { get; set; }

    public int? IdTeacher { get; set; }

    public virtual Teacher? IdTeacherNavigation { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
