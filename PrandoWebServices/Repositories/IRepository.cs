using PrandoWebServices.Data.Abstractions;
using PrandoWebServices.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PrandoWebServices.Repositories
{
    public interface IRepository<T> where T : class, IParentEntity
    {
        Task Add(T t);
        void Delete(T t);
        void Update(T t);
        List<T> FindAll();
        List<T> FindAll(Expression<Func<T, bool>> func);
        T FindById(int id);
        void DeleteAll(Func<T, bool> func);
        IQueryable<T> SqlStoredProcedure(string storedProcedureName, params object[] parameters)
    }
}
