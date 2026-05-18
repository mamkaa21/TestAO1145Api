using System;
using System.Collections.Generic;

namespace TestAO1145Api;

public partial class Question
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Aswer> Aswers { get; set; } = new List<Aswer>();

    public virtual ICollection<Test> Tests { get; set; } = new List<Test>();
}
