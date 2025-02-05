using System;
using System.Collections.Generic;

namespace BlazorApp2.DB;

public partial class Cabinet
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
