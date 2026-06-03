using System;
using System.Collections.Generic;

namespace TestAO1145Api;

public partial class Studentanswer
{
    public int Id { get; set; }

    public int? IdStudent { get; set; }

    public int? IdTest { get; set; }

    public DateTime? DateTime { get; set; }

    public int? IdMark { get; set; }

    public virtual Mark? IdMarkNavigation { get; set; }

    public virtual Student? IdStudentNavigation { get; set; }

    public virtual Test? IdTestNavigation { get; set; }

    public virtual ICollection<Testcrossquestion> Testcrossquestions { get; set; } = new List<Testcrossquestion>();
}
