using System.ComponentModel.DataAnnotations;

namespace GenericRepository.Abstractions.Entities
{
    public interface IEntity
    {
        public int Id { get; set; }
    }

    public abstract class EntityBase : IEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
