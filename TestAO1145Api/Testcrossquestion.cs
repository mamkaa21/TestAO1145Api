using System;
using System.Collections.Generic;

namespace TestAO1145Api;

public partial class Testcrossquestion
{
    public int IdStudent { get; set; }

    public int IdQuestion { get; set; }

    public int IdAnswer { get; set; }

    public virtual Answer IdAnswerNavigation { get; set; } = null!;

    public virtual Question IdQuestionNavigation { get; set; } = null!;

    public virtual Studentanswer IdStudentNavigation { get; set; } = null!;
}
