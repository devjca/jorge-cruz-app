using DataAccess.Context;
using DataAccess.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DataAccess
{
    public class GenericRepository: IGenericRepository
    {

        private readonly AspenCapitalContext _aspenCapitalContext;

        public GenericRepository(AspenCapitalContext aspenCapitalContext)
        {
            _aspenCapitalContext = aspenCapitalContext;
        }

        public bool Delete<T>(object Id) where T : class
        {
            bool result = false;
            try
            {
                var currenEntry = _aspenCapitalContext.Set<T>().Find(Id);
                _aspenCapitalContext.Set<T>().Remove(currenEntry);
                _aspenCapitalContext.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {

                throw;
            }

            return result;
        }

        public List<T> GetAll<T>() where T : class
        {
            List<T> result = new List<T>();

            try
            {
                result = _aspenCapitalContext.Set<T>().ToList();
            }
            catch (Exception ex)
            {

                throw;
            }

            return result;
        }

        public T GetbyId<T>(int Id) where T : class
        {
            try
            {
                return _aspenCapitalContext.Set<T>().Find(Id);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public bool Save<T>(T Entity) where T : class
        {
            bool result = false;
            try
            {
                _aspenCapitalContext.Set<T>().Add(Entity);
                _aspenCapitalContext.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public bool Update<T>(T Entity, object Id) where T : class
        {
            bool result = false;
            var entry = _aspenCapitalContext.Entry(Entity);

            if (entry.State == Microsoft.EntityFrameworkCore.EntityState.Detached)
            {
                var currentEntry = _aspenCapitalContext.Set<T>().Find(Id);
                if (currentEntry != null)
                {
                    var attachedEntry = this._aspenCapitalContext.Entry(currentEntry);
                    attachedEntry.CurrentValues.SetValues(Entity);
                }
                else
                {
                    _aspenCapitalContext.Set<T>().Attach(Entity);
                    entry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
            }
            try
            {
                _aspenCapitalContext.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {


            }

            return result;
        }
    }
}
