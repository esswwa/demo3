using System;
using System.Collections.Generic;

namespace demo3.Models;

public partial class Application
{
    public int IdApplication { get; set; }

    public string Description { get; set; } = null!;

    public string PhaseComplete { get; set; } = null!;

    public string StatusApplication { get; set; } = null!;

    public int IdTypeMalfunction { get; set; }

    public int IdEquipment { get; set; }

    public string FullName { get; set; } = null!;

    public double PriceApplication { get; set; }

    public string? CommentExecutor { get; set; }

    public DateOnly DateAddApplication { get; set; }

    public DateOnly? DateEndApplication { get; set; }

    public string? TimeComplete { get; set; }

    public virtual ICollection<Executor> Executors { get; set; } = new List<Executor>();

    public virtual Equipment IdEquipmentNavigation { get; set; } = null!;

    public virtual TypeMalfunction IdTypeMalfunctionNavigation { get; set; } = null!;
}
