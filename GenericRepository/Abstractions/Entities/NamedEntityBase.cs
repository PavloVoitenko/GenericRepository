
namespace GenericRepository.Abstractions.Entities
{
    public interface INamedEntity : IEntity
    {
        public string Name { get; set; }
    }
    public abstract class NamedEntityBase : EntityBase, INamedEntity
    {
        public string Name { get; set; }
    }
}
