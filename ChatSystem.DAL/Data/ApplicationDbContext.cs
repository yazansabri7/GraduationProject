using ChatSystem.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatSystem.DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<DoctorPatient> DoctorPatients { get; set; }
        public DbSet<Reading> Readings { get; set; }
        public DbSet<BloodPressure> BloodPressures { get; set; }
        public DbSet<GlucoseReading> GlucoseReadings { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<MedicationAuditLog> MedicationAuditLogs { get; set; }
        public DbSet<Alert> Alerts { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");

            builder.Entity<DoctorPatient>()
                .HasOne(dp => dp.Patient)
                .WithMany(p => p.DoctorPatients)
                .HasForeignKey(dp => dp.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<DoctorPatient>()
                .HasOne(dp => dp.Doctor)
                .WithMany(d => d.DoctorPatients)
                .HasForeignKey(dp => dp.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Chat>()
                .HasOne(c => c.DoctorPatient)
                .WithOne()
                .HasForeignKey<Chat>(c => new { c.DoctorId, c.PatientId })
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
                .HasOne(m => m.Chat)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ChatId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Medication>()
                .HasOne(m => m.CreatedByUser)
                .WithMany()
                .HasForeignKey(m => m.CreatedByUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Medication>()
                .HasOne(m => m.UpdatedByUser)
                .WithMany()
                .HasForeignKey(m => m.UpdatedByUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<MedicationAuditLog>()
                .Property(log => log.Action)
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Entity<MedicationAuditLog>()
                .HasIndex(log => new { log.PatientId, log.OccurredAt });
            builder.Entity<MedicationAuditLog>()
                .HasIndex(log => new { log.MedicationId, log.OccurredAt });
        }
    }
}
