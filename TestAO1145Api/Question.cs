using System;
using System.Collections.Generic;

namespace TestAO1145Api;

public partial class Question
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? IdTest { get; set; }

    public int? Type { get; set; }

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual Test? IdTestNavigation { get; set; }

    public virtual ICollection<Testcrossquestion> Testcrossquestions { get; set; } = new List<Testcrossquestion>();
}
