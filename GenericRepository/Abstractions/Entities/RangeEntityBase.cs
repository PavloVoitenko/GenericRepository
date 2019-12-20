using System;

namespace GenericRepository.Abstractions.Entities
{
    public interface IRangeEntity<TRange> : IEntity
    {
        public TRange RangeFrom { get; set; }
        public TRange RangeTo { get; set; }
    }
    public abstract class DateStateEntityBase : EntityBase, IRangeEntity<DateTime>
    {
        public DateTime RangeFrom { get; set; }
        public DateTime RangeTo { get; set; }
    }

    public abstract class RangeEntityBase : EntityBase, IRangeEntity<TimeSpan>
    {
        public TimeSpan RangeFrom { get; set; }
        public TimeSpan RangeTo { get; set; }
    }
}
