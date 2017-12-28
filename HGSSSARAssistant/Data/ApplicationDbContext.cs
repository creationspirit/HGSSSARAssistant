using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HGSSSARAssistant.Models;

namespace HGSSSARAssistant.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<Station> Stations { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Models.Action> Actions{ get; set; }
        public DbSet<ActionType> ActionTypes { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<RescuerAction> RescuerActions { get; set; }
        public DbSet<RescuerSkill> RescuerSkills { get; set; }
        public DbSet<Skill> Skills { get; set; }
    }
}
