using HGSSSARAssistant.Core;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HGSSSARAssistant.DAL.EF
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<HGSSSARAssistant.Core.User> Users { get; set; }
        public DbSet<HGSSSARAssistant.Core.Station> Stations { get; set; }
        public DbSet<HGSSSARAssistant.Core.Role> Roles { get; set; }
        public DbSet<HGSSSARAssistant.Core.MessageTemplate> MessageTemplates { get; set; }
        public DbSet<HGSSSARAssistant.Core.Location> Locations { get; set; }
        public DbSet<HGSSSARAssistant.Core.Expertise> Expertises { get; set; }
        public DbSet<HGSSSARAssistant.Core.Category> Categories { get; set; }
        public DbSet<HGSSSARAssistant.Core.Availability> Availabilities { get; set; }
        public DbSet<HGSSSARAssistant.Core.ActionType> ActionTypes { get; set; }
        public DbSet<HGSSSARAssistant.Core.Action> Actions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<HGSSSARAssistant.Core.Category>().Property(c => c.Name).IsRequired();
            modelBuilder.Entity<HGSSSARAssistant.Core.ActionType>().Property(at => at.Name).IsRequired();
            modelBuilder.Entity<HGSSSARAssistant.Core.Expertise>().Property(e => e.Name).IsRequired();
            modelBuilder.Entity<HGSSSARAssistant.Core.Role>().Property(r => r.Name).IsRequired();

            modelBuilder.Entity<HGSSSARAssistant.Core.UserExpertise>()
                .HasKey(ue => new { ue.UserId, ue.ExpertiseId });

            modelBuilder.Entity<HGSSSARAssistant.Core.UserExpertise>()
                .HasOne(ue => ue.User)
                .WithMany(u => u.UserExpertise)
                .HasForeignKey(ue => ue.UserId);

            modelBuilder.Entity<HGSSSARAssistant.Core.UserExpertise>()
                .HasOne(ue => ue.Expertise)
                .WithMany(e => e.UserExpertise)
                .HasForeignKey(ue => ue.ExpertiseId);
            

            modelBuilder.Entity<HGSSSARAssistant.Core.User>(user =>
            {
                user.HasKey(u => u.Id);
                user.Property(u => u.FirstName);
                user.Property(u => u.LastName);
                user.HasOne(u => u.Address);
                user.HasOne(u => u.Role);
                user.HasOne(u => u.Station);
                user.HasOne(u => u.Category);

                user.HasMany(u => u.Availiabilities)
                    .WithOne();

                user.HasMany(u => u.UserExpertise)
                    .WithOne(ue => ue.User);
            });


            modelBuilder.Entity<HGSSSARAssistant.Core.Action>(action =>
            {
                action.Property(a => a.Name).IsRequired();
                action.Property(a => a.MeetupTime).IsRequired();

                action.HasOne(a => a.Location);
                action.HasOne(a => a.Leader);

                action.HasMany(a => a.InvitedRescuers)
                    .WithOne();
                action.HasMany(a => a.AttendedRescuers)
                    .WithOne();
            });


            modelBuilder.Entity<HGSSSARAssistant.Core.Availability>(availability =>
            {
                availability.Property(a => a.StartTime).IsRequired();
                availability.Property(a => a.EndTime).IsRequired();
                availability.HasOne(a => a.Location);
                availability.Property(a => a.Day);
            });

            modelBuilder.Entity<HGSSSARAssistant.Core.Station>(station =>
            {
                station.Property(s => s.Name).IsRequired();
                station.HasOne(s => s.Location);
            });

            modelBuilder.Entity<HGSSSARAssistant.Core.Location>(loc =>
            {
                loc.Property(l => l.Name);
                loc.Property(l => l.Latitude).IsRequired();
                loc.Property(l => l.Longitude).IsRequired();
            });


        }
    }
}
