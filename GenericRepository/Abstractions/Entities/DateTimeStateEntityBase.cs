using System;

namespace GenericRepository.Abstractions.Entities
{
    public interface IDateTimeEntity : IRangeEntity<TimeSpan>
    {
        public DateTime AsOfDate { get; set; }
    }

    public class DateTimeStateEntityBase : RangeEntityBase, IDateTimeEntity
    {
        public DateTime AsOfDate { get; set ; }
    }
}
