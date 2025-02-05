using System;
using System.Collections.Generic;

namespace BlazorApp2.DB;

public partial class EventCalendar
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public int? IdType { get; set; }

    public int? IdStatus { get; set; }

    public DateTime? DateStart { get; set; }

    public DateTime? DateEnd { get; set; }

    public string? Description { get; set; }

    public virtual StatusEvent? IdStatusNavigation { get; set; }

    public virtual TypeEvent? IdTypeNavigation { get; set; }

    public virtual ICollection<Department> IdDepartments { get; set; } = new List<Department>();

    public virtual ICollection<Employee> IdEmployees { get; set; } = new List<Employee>();

    public virtual ICollection<Material> IdMaterials { get; set; } = new List<Material>();
}
