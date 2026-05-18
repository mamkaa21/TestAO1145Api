using System;
using System.Collections.Generic;

namespace TestAO1145Api;

public partial class Teachercrosssubject
{
    public int? IdTeacher { get; set; }

    public int? IdSubject { get; set; }

    public virtual Subject? IdSubjectNavigation { get; set; }

    public virtual Teacher? IdTeacherNavigation { get; set; }
}
