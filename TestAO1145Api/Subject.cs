using System;
using System.Collections.Generic;

namespace TestAO1145Api;

public partial class Subject
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Test> Tests { get; set; } = new List<Test>();

    public virtual ICollection<Teacher> IdTeachers { get; set; } = new List<Teacher>();
}
