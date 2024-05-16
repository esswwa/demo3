using System;
using System.Collections.Generic;

namespace demo3.Models;

public partial class User
{
    public int IdUser { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public int IdRole { get; set; }

    public virtual ICollection<Executor> Executors { get; set; } = new List<Executor>();

    public virtual Role IdRoleNavigation { get; set; } = null!;
}
