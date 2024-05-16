using System;
using System.Collections.Generic;

namespace demo3.Models;

public partial class Executor
{
    public int IdExecutor { get; set; }

    public int IdApplication { get; set; }

    public int IdUser { get; set; }

    public virtual Application IdApplicationNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
