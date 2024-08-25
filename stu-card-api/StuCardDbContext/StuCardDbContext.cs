using Microsoft.EntityFrameworkCore;
using stu_card_api.Entitys;
using stu_card_entity_store.Store;

namespace stu_card_api.StuDbContext
{
    public class StuCardDbContext : DbContext
    {
        IConfiguration configuration;
        public StuCardDbContext(DbContextOptions<StuCardDbContext> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Entity<TemplateEntity>(modelBuilder);
            Entity<TemplateItemEntity>(modelBuilder);
            Entity<TemplateItemShadowEntity>(modelBuilder);
            Entity<TemplateItemFabricEntity>(modelBuilder);
            Entity<FileEntity>(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void Entity<T>(ModelBuilder modelBuilder) where T : CommonEntity, IDeleteStore
        {
            modelBuilder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connection = configuration.GetSection("ConnectionString").Value;
            optionsBuilder.UseMySql(connection, ServerVersion.AutoDetect(connection));
            base.OnConfiguring(optionsBuilder);
        }
    }
}
