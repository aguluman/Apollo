using System.Dynamic;
using Entities.Models;

namespace Contracts;

public interface IDataShaper<T>
{
    IEnumerable<ShapedEntity> ShapeData(IEnumerable<T> shapedEntities, string fieldsString);

    ShapedEntity ShapeData(T shapedEntity, string fieldsString);
}