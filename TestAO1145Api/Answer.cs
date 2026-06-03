using System;
using System.Collections.Generic;

namespace TestAO1145Api;

public partial class Answer
{
    public int Id { get; set; }

    public string? Text { get; set; }

    public int? IdQuestion { get; set; }

    public bool? RightAnswer { get; set; }

    public virtual Question? IdQuestionNavigation { get; set; }

    public virtual ICollection<Testcrossquestion> Testcrossquestions { get; set; } = new List<Testcrossquestion>();
}
