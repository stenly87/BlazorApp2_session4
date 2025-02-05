using System;
using System.Collections.Generic;

namespace BlazorApp2.DB;

public partial class Employee
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Patronymic { get; set; }

    public int? IdDepartment { get; set; }

    public int? IdRole { get; set; }

    public string WorkPhone { get; set; } = null!;

    public string? PersonalPhone { get; set; }

    public int? IdCabinet { get; set; }

    public string CorporateEmail { get; set; } = null!;

    public DateTime? BirthdayDate { get; set; }

    public string? Description { get; set; }

    public int? IdHelper { get; set; }

    public int? IdLeader { get; set; }

    public virtual ICollection<AbsenceCalendar> AbsenceCalendars { get; set; } = new List<AbsenceCalendar>();

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    public virtual Cabinet? IdCabinetNavigation { get; set; }

    public virtual Department? IdDepartmentNavigation { get; set; }

    public virtual Employee? IdHelperNavigation { get; set; }

    public virtual Employee? IdLeaderNavigation { get; set; }

    public virtual Role? IdRoleNavigation { get; set; }

    public virtual ICollection<Employee> InverseIdHelperNavigation { get; set; } = new List<Employee>();

    public virtual ICollection<Employee> InverseIdLeaderNavigation { get; set; } = new List<Employee>();

    public virtual ICollection<EventCalendar> IdEventCalendars { get; set; } = new List<EventCalendar>();
}
