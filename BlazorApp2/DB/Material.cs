using System;
using System.Collections.Generic;

namespace BlazorApp2.DB;

public partial class Material
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public DateTime? DateApproval { get; set; }

    public DateTime? DateChange { get; set; }

    public int? IdStatus { get; set; }

    public int? IdType { get; set; }

    public string? Field { get; set; }

    public string? Author { get; set; }

    public virtual StatusMaterial? IdStatusNavigation { get; set; }

    public virtual TypeMaterial? IdTypeNavigation { get; set; }

    public virtual ICollection<EventCalendar> IdEventCalendars { get; set; } = new List<EventCalendar>();
}
