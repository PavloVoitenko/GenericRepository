using MetadataApi.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace MetadataApi.Model
{
    public abstract class MetadataContextBase : DbContext
    {
        public MetadataContextBase(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        protected abstract


        public DbSet<MetaEntity> MetaEntities { get; set; }
        public DbSet<MetaEntityField> MetaField { get; set; }
    }
}
