using ChatSystem.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Security.Claims;
using System.Text;

namespace ChatSystem.DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public readonly IHttpContextAccessor _httpContextAccessor;
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<DoctorPatient> DoctorPatients { get; set; }
        public DbSet<Reading> Readings { get; set; }
        public DbSet<BloodPressure> BloodPressures { get; set; }
        public DbSet<GlucoseReading> GlucoseReadings { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<Alert> Alerts { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options , 
            IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

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

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<Medication>();
            if (_httpContextAccessor.HttpContext != null)
            {

                var CurrentUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                foreach (var entry in entries)
                {
                    if (entry.State == EntityState.Added)
                    {
                        entry.Property(x => x.CreatedAt).CurrentValue = DateTime.UtcNow;
                        entry.Property(x => x.CreatedBy).CurrentValue = CurrentUserId;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        entry.Property(x => x.UpdatedAt).CurrentValue = DateTime.UtcNow;
                        entry.Property(x => x.UpdatedBy).CurrentValue = CurrentUserId;
                    }

                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        public override int SaveChanges()
        {
            var entires = ChangeTracker.Entries<Medication>();
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            foreach (var entry in entires)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property(e => e.CreatedAt).CurrentValue = DateTime.UtcNow;
                    entry.Property(e => e.CreatedBy).CurrentValue = userId;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Property(e => e.UpdatedAt).CurrentValue = DateTime.UtcNow;
                    entry.Property(e => e.UpdatedBy).CurrentValue = userId;
                }
            }
            return base.SaveChanges();
        }
    }
}
