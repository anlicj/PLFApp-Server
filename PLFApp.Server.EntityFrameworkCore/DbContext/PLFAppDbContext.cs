using Microsoft.EntityFrameworkCore;
using PLFApp.Server.Core;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace PLFApp.Server.EntityFrameworkCore
{
    public class PLFAppDbContext : DbContext
    {
        public PLFAppDbContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<Goods> Goods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var allEntitys = this.GetType().GetProperties();
            var keyOfIntEntitys = allEntitys.Where(t => t.GetType().IsSubclassOf(typeof(BaseEntity<int>)));
            foreach (var entity in keyOfIntEntitys)
            {
                modelBuilder.Entity(entity.Name).Property("Id").HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.Identity);
            }
            var softDeleteEntitys = allEntitys.Where(t => typeof(ISoftDelete).IsAssignableFrom(t.GetType()) && t.GetType().IsClass && !t.GetType().IsAbstract && !t.GetType().IsGenericType);
            foreach (var entity in softDeleteEntitys)
            {
                modelBuilder.Entity(entity.Name).HasIndex("IsDelete");
            }
            modelBuilder.ApplyConfiguration(new MemberConfig());
            modelBuilder.ApplyConfiguration(new GoodsConfig());
            base.OnModelCreating(modelBuilder);
        }
    }
}
