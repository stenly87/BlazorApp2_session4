using System;
using System.Collections.Generic;

namespace BlazorApp2.DB;

public partial class Department
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public int? IdParent { get; set; }

    public string? Description { get; set; }

    public int? IdLeader { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual Employee? IdLeaderNavigation { get; set; }

    public virtual Department? IdParentNavigation { get; set; }

    public virtual ICollection<Department> InverseIdParentNavigation { get; set; } = new List<Department>();

    public virtual ICollection<EventCalendar> IdEventCalendars { get; set; } = new List<EventCalendar>();
}
