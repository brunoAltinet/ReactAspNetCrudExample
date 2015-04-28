using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using AltiFinReact.Core.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AltiFinReact.Core.Services
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    public class AppDbContext : IdentityDbContext<AppUser, AppRole, int, AppUserLogin, AppUserRole, AppUserClaim>
    {
        public AppDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<Partner> Partners { get; set; }

        public DbSet<InvoiceItem> InvoiceItems { get; set; }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AppUser>().ToTable("AppUser").Property(p => p.Id).HasColumnName("Id");
            modelBuilder.Entity<AppUserRole>().ToTable("AppUserRole");
            modelBuilder.Entity<AppUserLogin>().ToTable("AppUserLogin");
            modelBuilder.Entity<AppUserClaim>().ToTable("AppUserClaim");
            modelBuilder.Entity<AppRole>().ToTable("AppRole");
            modelBuilder.Entity<InvoiceItem>().HasRequired(x => x.Invoice)
                .WithMany(x => x.InvoiceItems).HasForeignKey(x => x.InvoiceId);
            //modelBuilder.Entity<SimpleDoc>().ToTable("SimpleDoc");
        }
    }
}