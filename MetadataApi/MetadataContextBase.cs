using MetadataApi.Model.Entities;

using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MetadataApi.Model
{
    public abstract class MetadataContextBase<TMeta> : DbContext where TMeta : class
    {
        public MetadataContextBase(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var metaBuilder = new MetadataBuilder<TMeta>();
            metaBuilder.BuildMetadata();

            modelBuilder.Entity<MetaEntity>().HasData(metaBuilder.Entities.ToList());
            modelBuilder.Entity<MetaEntityField>().HasData(metaBuilder.Fields.ToList());
        }

        public DbSet<MetaEntity> MetaEntities { get; set; }
        public DbSet<MetaEntityField> MetaField { get; set; }
    }
}
