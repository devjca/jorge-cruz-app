using System.Collections.Generic;

namespace DataAccess.Contracts
{
    public interface IGenericRepository
    {
        List<T> GetAll<T>() where T : class;

        T GetbyId<T>(int Id) where T : class;
        bool Save<T>(T Entity) where T : class;

        bool Update<T>(T Entity, object Id) where T : class;

        bool Delete<T>(object Id) where T : class;
    }
}
