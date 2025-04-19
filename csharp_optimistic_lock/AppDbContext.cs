using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_optimistic_lock
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tbl_Blog>()
                .Property(b => b.RowVersion)
                .IsRowVersion();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=MoeThetKhine;User ID=sa;Password=sasa@123;TrustServerCertificate=True;");
        }

        public DbSet<Tbl_Blog> TblBlogs { get; set; }
    }
}
