using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ZKKDotNetCore.DbFirstCommandApp.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblStudent> TblStudents { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=.;Database=ZKKDotNetCore;User ID=sa;Password=sasa;Trusted_Connection=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblStudent>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK_Tbl_Student_1");

            entity.ToTable("Tbl_Student");

            entity.Property(e => e.StudentId).HasColumnName("Student_Id");
            entity.Property(e => e.StudentCity)
                .HasMaxLength(50)
                .HasColumnName("Student_City");
            entity.Property(e => e.StudentGender)
                .HasMaxLength(50)
                .HasColumnName("Student_Gender");
            entity.Property(e => e.StudentName)
                .HasMaxLength(50)
                .HasColumnName("Student_Name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
