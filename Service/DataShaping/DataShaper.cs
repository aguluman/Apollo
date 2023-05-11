using System.Dynamic;
using System.Reflection;
using Contracts;
using Entities.Models;

namespace Service.DataShaping;

public class DataShaper<T> : IDataShaper<T> where T : class
{
    private PropertyInfo[] Properties { get; set; }

    public DataShaper()
    {
        Properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
    }

    public IEnumerable<ShapedEntity> ShapeData(IEnumerable<T> shapedEntities, string fieldsString)
    {
        var requiredProperties = GetRequiredProperties(fieldsString);
        return FetchData(shapedEntities, requiredProperties);
    }

    public ShapedEntity ShapeData(T shapedEntity, string fieldsString)
    {
        var requiredProperties = GetRequiredProperties(fieldsString);

        return FetchDataForEntity(shapedEntity, requiredProperties);
    }

    
    private IEnumerable<PropertyInfo> GetRequiredProperties(string fieldsString)
    {
        var requiredProperties = new List<PropertyInfo>();

        if (!string.IsNullOrWhiteSpace(fieldsString))
        {
            var fields = fieldsString.Split(',', StringSplitOptions.RemoveEmptyEntries);

            foreach (var field in fields)
            {
                var property = Properties.FirstOrDefault(pi => pi.Name.Equals(field.Trim(),
                    StringComparison.InvariantCultureIgnoreCase));
                
                if(property is null)
                    continue;
                
                requiredProperties.Add(property);
            }
        }
        else
        {
            requiredProperties = Properties.ToList();
        }

        return requiredProperties;
    }


    private static IEnumerable<ShapedEntity> FetchData(IEnumerable<T> shapedEntities, IEnumerable<PropertyInfo> requiredProperties)
    {
        return shapedEntities.Select(shapedEntity => FetchDataForEntity(shapedEntity, requiredProperties)).ToList();
    }

    
    private static ShapedEntity FetchDataForEntity(T shapedEntity, IEnumerable<PropertyInfo> requiredProperties)
    {
        var shapedObject = new ShapedEntity();

        foreach (var property in requiredProperties)
        {
            var objectPropertyValue = property.GetValue(shapedEntity);
            shapedObject.Entity.TryAdd(property.Name, objectPropertyValue);
        }

        var objectProperty = shapedEntity.GetType().GetProperty("Id");
        shapedObject.Id = (Guid)objectProperty.GetValue(shapedEntity);
        
        return shapedObject;
    }
}