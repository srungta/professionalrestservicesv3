using System;
using System.Collections.Generic;

namespace Microsoft.Libraries.DataAccess.Interfaces
{
    public interface IRepository<EntityType, EntityKey> : IDisposable
            where EntityType : class
    {
        IEnumerable<EntityType> GetAllEntities();
        EntityType GetEntityByKey(EntityKey entityKey);
        bool AddNewEntity(EntityType entityType);
    }
}
