using System;
using System.Collections.Generic;

namespace TestAO1145Api;

public partial class Mark
{
    public int Id { get; set; }

    public int? Number { get; set; }

    public int? CountQ { get; set; }

    public virtual ICollection<Studentanswer> Studentanswers { get; set; } = new List<Studentanswer>();
}
