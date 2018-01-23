using HGSSSARAssistant.Core;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HGSSSARAssistant.DAL.EF
{
    public class ApplicationContext : IdentityDbContext<User, Role, long>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

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

            modelBuilder.Entity<HGSSSARAssistant.Core.User>(user =>
            {
                user.HasKey(u => u.Id);
                user.Property(u => u.FirstName);
                user.Property(u => u.LastName);
                user.Property(u => u.Address);
                user.HasOne(u => u.Role);
                user.HasOne(u => u.Station);
                user.HasOne(u => u.Category);
                user.HasMany(u => u.Expertise);
            });
        }
    }
}
