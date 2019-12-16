using System;

namespace GenericRepository.Abstractions.Entities
{
    public interface IRangeEntity<TRange> : IEntity
    {
        public TRange ValidFrom { get; set; }
        public TRange ValidTo { get; set; }
    }
    public abstract class DateStateEntityBase : EntityBase, IRangeEntity<DateTime>
    {
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
    }

    public abstract class TimeStateEntityBase : EntityBase, IRangeEntity<TimeSpan>
    {
        public TimeSpan ValidFrom { get; set; }
        public TimeSpan ValidTo { get; set; }
    }
}
