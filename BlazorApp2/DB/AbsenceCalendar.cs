using System;
using System.Collections.Generic;

namespace BlazorApp2.DB;

public partial class AbsenceCalendar
{
    public int Id { get; set; }

    public int? IdType { get; set; }

    public DateTime? DateStart { get; set; }

    public DateTime? DateEnd { get; set; }

    public int? IdEmployee { get; set; }

    public string? Description { get; set; }

    public virtual Employee? IdEmployeeNavigation { get; set; }

    public virtual TypeAbsence? IdTypeNavigation { get; set; }
}
