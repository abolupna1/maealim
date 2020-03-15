using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using maealim.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace maealim.Data
{
   
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, int,
      IdentityUserClaim<int>,
      AppUserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

   
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            foreach (var foreignKey in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

            builder.Entity<AppUserRole>(

                userRole =>
                {
                    userRole.HasKey(ur => new { ur.UserId, ur.RoleId });
                    userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles).HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                    userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles).HasForeignKey(ur => ur.UserId)
                    .IsRequired();
                } );
         }

        public DbSet<Season> Seasons { get; set; }
        public DbSet<Jop> Jops { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeContract> EmployeeContracts { get; set; }
        public DbSet<WorkSeason> WorkSeasons { get; set; }
        public DbSet<Continent> Continents { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<College> Colleges { get; set; }
        public DbSet<Stage> Stages { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<MGuide> MGuides { get; set; }
        public DbSet<GuideContract> GuideContracts { get; set; }
        public DbSet<TypeOfProduct> TypeOfProducts { get; set; }
        public DbSet<ItemOfProduct> ItemOfProducts { get; set; }
        public DbSet<ItemExport> ItemExports { get; set; }
        public DbSet<ItemImport> ItemImports { get; set; }
        public DbSet<TypeNotable> TypeNotables { get; set; }
        public DbSet<JobNotable> JobNotable { get; set; }
        public DbSet<Sheikh> Sheikhs { get; set; }
        public DbSet<GuestReservation> GuestReservations { get; set; }
        public DbSet<Notable> Notables { get; set; }
        public DbSet<Gift> Gifts { get; set; }
        public DbSet<TypesMessage> TypesMessages { get; set; }
        public DbSet<WjhaaMessage> WjhaaMessages { get; set; }
        public DbSet<MessageSend> MessageSends { get; set; }
        public DbSet<Attend> Attends { get; set; }




    }
}
