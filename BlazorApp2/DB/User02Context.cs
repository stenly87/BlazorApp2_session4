using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp2.DB;

public partial class User02Context : DbContext
{
    public User02Context()
    {
    }

    public User02Context(DbContextOptions<User02Context> options)
        : base(options)
    {
    }

    public virtual DbSet<AbsenceCalendar> AbsenceCalendars { get; set; }

    public virtual DbSet<Cabinet> Cabinets { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EventCalendar> EventCalendars { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<StatusEvent> StatusEvents { get; set; }

    public virtual DbSet<StatusMaterial> StatusMaterials { get; set; }

    public virtual DbSet<TypeAbsence> TypeAbsences { get; set; }

    public virtual DbSet<TypeEvent> TypeEvents { get; set; }

    public virtual DbSet<TypeMaterial> TypeMaterials { get; set; }

    public virtual DbSet<WorkingCalendar> WorkingCalendars { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=192.168.200.35;user=user02;database=user02;password=77053;trustservercertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AbsenceCalendar>(entity =>
        {
            entity.ToTable("AbsenceCalendar");

            entity.Property(e => e.DateEnd).HasColumnType("datetime");
            entity.Property(e => e.DateStart).HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");

            entity.HasOne(d => d.IdEmployeeNavigation).WithMany(p => p.AbsenceCalendars)
                .HasForeignKey(d => d.IdEmployee)
                .HasConstraintName("FK_AbsenceCalendar_Employee");

            entity.HasOne(d => d.IdTypeNavigation).WithMany(p => p.AbsenceCalendars)
                .HasForeignKey(d => d.IdType)
                .HasConstraintName("FK_AbsenceCalendar_TypeAbsence");
        });

        modelBuilder.Entity<Cabinet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_cabinet");

            entity.ToTable("Cabinet");

            entity.Property(e => e.Title).HasMaxLength(10);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_department");

            entity.ToTable("Department");

            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Title).HasMaxLength(250);

            entity.HasOne(d => d.IdLeaderNavigation).WithMany(p => p.Departments)
                .HasForeignKey(d => d.IdLeader)
                .HasConstraintName("FK_Department_Employee");

            entity.HasOne(d => d.IdParentNavigation).WithMany(p => p.InverseIdParentNavigation)
                .HasForeignKey(d => d.IdParent)
                .HasConstraintName("FK_Department_Department");

            entity.HasMany(d => d.IdEventCalendars).WithMany(p => p.IdDepartments)
                .UsingEntity<Dictionary<string, object>>(
                    "CalendarDepartment",
                    r => r.HasOne<EventCalendar>().WithMany()
                        .HasForeignKey("IdEventCalendar")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CalendarDepartment_EventCalendar"),
                    l => l.HasOne<Department>().WithMany()
                        .HasForeignKey("IdDepartment")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CalendarDepartment_Department"),
                    j =>
                    {
                        j.HasKey("IdDepartment", "IdEventCalendar");
                        j.ToTable("CalendarDepartment");
                    });
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_employee");

            entity.ToTable("Employee");

            entity.Property(e => e.BirthdayDate).HasColumnType("datetime");
            entity.Property(e => e.CorporateEmail).HasMaxLength(50);
            entity.Property(e => e.Description).HasColumnType("ntext");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Patronymic).HasMaxLength(50);
            entity.Property(e => e.PersonalPhone).HasMaxLength(20);
            entity.Property(e => e.WorkPhone).HasMaxLength(20);

            entity.HasOne(d => d.IdCabinetNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.IdCabinet)
                .HasConstraintName("FK_Employee_Cabinet");

            entity.HasOne(d => d.IdDepartmentNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.IdDepartment)
                .HasConstraintName("FK_Employee_Department");

            entity.HasOne(d => d.IdHelperNavigation).WithMany(p => p.InverseIdHelperNavigation)
                .HasForeignKey(d => d.IdHelper)
                .HasConstraintName("FK_Employee_Employee");

            entity.HasOne(d => d.IdLeaderNavigation).WithMany(p => p.InverseIdLeaderNavigation)
                .HasForeignKey(d => d.IdLeader)
                .HasConstraintName("FK_Employee_Employee1");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.IdRole)
                .HasConstraintName("FK_Employee_Role");

            entity.HasMany(d => d.IdEventCalendars).WithMany(p => p.IdEmployees)
                .UsingEntity<Dictionary<string, object>>(
                    "CalendarEmployee",
                    r => r.HasOne<EventCalendar>().WithMany()
                        .HasForeignKey("IdEventCalendar")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CalendarEmployee_EventCalendar"),
                    l => l.HasOne<Employee>().WithMany()
                        .HasForeignKey("IdEmployee")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CalendarEmployee_Employee"),
                    j =>
                    {
                        j.HasKey("IdEmployee", "IdEventCalendar");
                        j.ToTable("CalendarEmployee");
                    });
        });

        modelBuilder.Entity<EventCalendar>(entity =>
        {
            entity.ToTable("EventCalendar");

            entity.Property(e => e.DateEnd).HasColumnType("datetime");
            entity.Property(e => e.DateStart).HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.IdStatusNavigation).WithMany(p => p.EventCalendars)
                .HasForeignKey(d => d.IdStatus)
                .HasConstraintName("FK_EventCalendar_StatusEvent");

            entity.HasOne(d => d.IdTypeNavigation).WithMany(p => p.EventCalendars)
                .HasForeignKey(d => d.IdType)
                .HasConstraintName("FK_EventCalendar_TypeEvent");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.ToTable("Material");

            entity.Property(e => e.Author).HasMaxLength(50);
            entity.Property(e => e.DateApproval).HasColumnType("datetime");
            entity.Property(e => e.DateChange).HasColumnType("datetime");
            entity.Property(e => e.Field).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.IdStatusNavigation).WithMany(p => p.Materials)
                .HasForeignKey(d => d.IdStatus)
                .HasConstraintName("FK_Material_StatusMaterial");

            entity.HasOne(d => d.IdTypeNavigation).WithMany(p => p.Materials)
                .HasForeignKey(d => d.IdType)
                .HasConstraintName("FK_Material_TypeMaterial");

            entity.HasMany(d => d.IdEventCalendars).WithMany(p => p.IdMaterials)
                .UsingEntity<Dictionary<string, object>>(
                    "CalendarMaterial",
                    r => r.HasOne<EventCalendar>().WithMany()
                        .HasForeignKey("IdEventCalendar")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CalendarMaterial_EventCalendar"),
                    l => l.HasOne<Material>().WithMany()
                        .HasForeignKey("IdMaterial")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CalendarMaterial_Material"),
                    j =>
                    {
                        j.HasKey("IdMaterial", "IdEventCalendar");
                        j.ToTable("CalendarMaterial");
                    });
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_role");

            entity.ToTable("Role");

            entity.Property(e => e.Title).HasMaxLength(150);
        });

        modelBuilder.Entity<StatusEvent>(entity =>
        {
            entity.ToTable("StatusEvent");

            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<StatusMaterial>(entity =>
        {
            entity.ToTable("StatusMaterial");

            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<TypeAbsence>(entity =>
        {
            entity.ToTable("TypeAbsence");

            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<TypeEvent>(entity =>
        {
            entity.ToTable("TypeEvent");

            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<TypeMaterial>(entity =>
        {
            entity.ToTable("TypeMaterial");

            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<WorkingCalendar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("WorkingCalendar_pk");

            entity.ToTable("WorkingCalendar", tb => tb.HasComment("Список дней исключений в производственном календаре"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("Идентификатор строки");
            entity.Property(e => e.ExceptionDate).HasComment("День-исключение");
            entity.Property(e => e.IsWorkingDay).HasComment("0 - будний день, но законодательно принят выходным");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
