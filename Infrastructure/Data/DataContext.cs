using Microsoft.EntityFrameworkCore;
using ReviewApp.Domain.Properties.Models;

namespace ReviewApp.Infrastructure.Data

{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Goods> Goods { get; set; }
        public DbSet<GoodsCategory> GoodsCategories { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GoodsCategory>()
                .HasKey(pc => new { pc.GoodId, pc.CategoryId });
            modelBuilder.Entity<GoodsCategory>()
                .HasOne(p => p.Goods)
                .WithMany(pc => pc.GoodsCategories)
                .HasForeignKey(p => p.GoodId);
            modelBuilder.Entity<GoodsCategory>()
                .HasOne(p => p.Category)
                .WithMany(pc => pc.GoodsCategories)
                .HasForeignKey(c => c.CategoryId);


        }
    }
}
