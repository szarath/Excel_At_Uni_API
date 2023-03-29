using Excel_At_Uni_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Excel_At_Uni_API.Data
{
    public class ExcelAtUniDbContext : DbContext
    {
        public ExcelAtUniDbContext(DbContextOptions<ExcelAtUniDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Enrolment> Enrolments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.StudentId);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.EmailAddress).IsRequired().HasMaxLength(100);
                entity.Property(e => e.IdNumber).IsRequired().HasMaxLength(13);
                entity.Property(e => e.ContactNumber).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Address).IsRequired();
                entity.Property(e => e.Province).IsRequired().HasMaxLength(50);
                entity.Property(e => e.PreferredMethodOfContact).IsRequired().HasMaxLength(50);
                entity.Property(e => e.ProfileImageFile).IsRequired();
            });

            modelBuilder.Entity<Enrolment>(entity =>
            {
                entity.HasKey(e => e.EnrolmentId);
                entity.Property(e => e.Institution).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Qualification).IsRequired().HasMaxLength(100);
                entity.Property(e => e.QualificationType).IsRequired().HasMaxLength(50);
                entity.Property(e => e.DateRegistered).IsRequired();
                entity.Property(e => e.GraduationDate).IsRequired();
                entity.Property(e => e.AverageToDate).IsRequired();
                entity.HasOne(e => e.Student).WithMany(s => s.Enrolments).HasForeignKey(e => e.StudentId);
            });
        }
    }
}
