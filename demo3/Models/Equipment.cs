using System;
using System.Collections.Generic;

namespace demo3.Models;

public partial class Equipment
{
    public int IdEquipment { get; set; }

    public string EquipmentName { get; set; } = null!;

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();
}
