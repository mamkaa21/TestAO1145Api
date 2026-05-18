using System;
using System.Collections.Generic;

namespace TestAO1145Api;

public partial class Test
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? IdSubject { get; set; }

    public int? IdQuestion { get; set; }

    public int? IdMark { get; set; }

    public int? IdTeacher { get; set; }

    public virtual Mark? IdMarkNavigation { get; set; }

    public virtual Question? IdQuestionNavigation { get; set; }

    public virtual Subject? IdSubjectNavigation { get; set; }

    public virtual Teacher? IdTeacherNavigation { get; set; }

    public virtual ICollection<Studentanswer> Studentanswers { get; set; } = new List<Studentanswer>();
}
