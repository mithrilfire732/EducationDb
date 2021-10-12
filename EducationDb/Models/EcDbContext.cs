using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationDb.Models
{
    public class EcDbContext : DbContext
    {
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Major> Majors { get; set; }
        
        
        public EcDbContext(DbContextOptions<EcDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder) {
            builder.Entity<Major>(e =>
            {
                e.ToTable("Majors"); //gives Table a name Overrides generated name
                e.HasKey(p => p.Id); // sets PK
                e.Property(p => p.Code).HasMaxLength(15).IsRequired(); // sets string length and not null 
                e.HasIndex(p => p.Code).IsUnique();                     // Sets a sql index and requires unique values for every row
                e.Property(p => p.Description).HasMaxLength(30).IsRequired();
                e.Property(p => p.MinSat);
            });
            builder.Entity<Student>(e =>
            {
                e.ToTable("Students");
                e.HasKey(p => p.Id);
                e.Property(p => p.Firstname).HasMaxLength(30).IsRequired();
                e.Property(p => p.Lastname).HasMaxLength(30).IsRequired();
                e.Property(p => p.SAT);
                e.Property(p => p.GPA).HasColumnType("decimal(5,1)");
                e.HasOne(p => p.Major)
                .WithMany(p => p.Students)
                .HasForeignKey(p=>p.MajorId)        //
                .OnDelete(DeleteBehavior.Restrict); // overrides default behavior so that if we delete a major it does not delete all attached students
            });
        }
    }
}
