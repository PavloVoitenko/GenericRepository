using MetadataApi.Attributes;
using MetadataApi.Attributes.Abstractions;
using MetadataApi.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MetadataApi
{
    class MetadataBuilder<T>
    {
        public IList<MetaEntity> Entities { get; private set; } = new List<MetaEntity>();
        public IList<MetaEntityField> Fields { get; private set; } = new List<MetaEntityField>();

        private IDictionary<DataType, IEnumerable<string>> _propertyDataTypes = new Dictionary<DataType, IEnumerable<string>>
        {
            { DataType.String, new [] { typeof(string).Name } },
            { DataType.Date, new [] { typeof(DateTime).Name } },
            { DataType.Time, new [] { typeof(TimeSpan).Name } },
            { DataType.Number, new [] { typeof(int).Name, typeof(double).Name, typeof(float).Name, typeof(decimal).Name } }
        };

        public void BuildMetadata()
        {
            var classes = typeof(T).Assembly.GetTypes().Where(t => t.GetCustomAttributes(typeof(MetaEntityAttribute), true).Length > 0);
            var maxEntityId = 1;
            var maxFieldId = 1;

            foreach (var type in classes)
            {
                var metaEntityAttribute = type.GetCustomAttributes(typeof(MetaEntityAttribute), true).FirstOrDefault() as MetaEntityAttribute;
                var metaEntity = new MetaEntity
                {
                    Id = maxEntityId,
                    Name = type.Name,
                    DisplayName = metaEntityAttribute.DisplayName
                };

                foreach (var property in type.GetProperties())
                {
                    var metaField = new MetaEntityField
                    {
                        Id = maxFieldId,
                        Name = property.Name,
                        DisplayName = property.Name,
                        Type = GetDataType(property),
                        Editable = EditableType.Editable,
                        Visible = true,
                        MetaEntityId = metaEntity.Id,
                        Entity = metaEntity
                    };


                    foreach (var attribute in property.GetCustomAttributes(typeof(MetaAttribute), true))
                    {
                        switch (attribute)
                        {
                            case DisplayAttribute da:
                                metaField.DisplayName = da.DisplayName;
                                break;
                            case EditableAttribute ea:
                                metaField.Editable = ea.Editable;
                                break;
                            case VisibleAttribute va:
                                metaField.Visible = va.Visible;
                                break;
                            default:
                                throw new Exception("Meta attribute not supported");
                        }
                    }

                    Fields.Add(metaField);
                    maxFieldId++;
                }

                Entities.Add(metaEntity);
                maxEntityId++;
            }
        }

        private DataType GetDataType(PropertyInfo property)
        {
            return _propertyDataTypes.FirstOrDefault(p => p.Value.Contains(property.PropertyType.Name)).Key;
        }
    }
}
