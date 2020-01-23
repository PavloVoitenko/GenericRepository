using GenericRepository.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GenericRepository.Helpers
{
    public static class IndexHelper
    {
        public static void BuildIndexes<TContext>(ModelBuilder modelBuilder) where TContext : DbContext
        {
            BuildNamedEntityIndexes<TContext>(modelBuilder);
        }

        private static void BuildNamedEntityIndexes<TContext>(ModelBuilder modelBuilder) where TContext : DbContext
        {
            var genericMethod = typeof(IndexHelper).GetMethod("AddNamedEntityKey", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            var namedProperties = typeof(TContext).GetProperties()
                                .Where(pi =>
                                        pi.PropertyType.IsGenericType &&
                                        pi.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>) &&
                                        pi.PropertyType.GetGenericArguments().First().IsSubclassOf(typeof(NamedEntityBase)));
            foreach (var np in namedProperties)
            {
                var genericType = np.PropertyType.GetGenericArguments().FirstOrDefault();
                var entityMethod = genericMethod.MakeGenericMethod(genericType);
                entityMethod.Invoke(null, new[] { modelBuilder });
            }
        }

        private static void AddNamedEntityKey<T>(ModelBuilder modelBuilder) where T : NamedEntityBase
        {
            modelBuilder.Entity<T>().HasIndex(e => e.Name);
        }
    }
}
