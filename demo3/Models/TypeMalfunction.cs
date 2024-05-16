using System;
using System.Collections.Generic;

namespace demo3.Models;

public partial class TypeMalfunction
{
    public int IdTypeMalfunction { get; set; }

    public string TypeMalfunctionName { get; set; } = null!;

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();
}
