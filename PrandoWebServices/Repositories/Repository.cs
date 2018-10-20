using PrandoWebServices.Data.Abstractions;
using PrandoWebServices.Data.Entities;
using PrandoWebServices.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CoreDbContext = Microsoft.EntityFrameworkCore.DbContext;


namespace PrandoWebServices.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IParentEntity
    {
        private readonly CoreDbContext _prandoDbContext;
        public Repository(CoreDbContext prandoDbContext)
        {
            _prandoDbContext = prandoDbContext;
        }

        public async Task Add(T t)
        {
            var addEntity = await _prandoDbContext.AddAsync(t);
        }

        public void Delete(T t)
        {
            if (t is SoftDeletable deletable)
            {
                deletable.IsDeleted = true;
                _prandoDbContext.Update(deletable);
            }
            else
                 _prandoDbContext.Set<T>().Remove(t);
        }

        public void DeleteAll(Func<T, bool> func)
        {
            var listToDelete = _prandoDbContext.Set<T>().Where(func);
            Array.ForEach(listToDelete.ToArray(), p =>{
                if (typeof(T) == typeof(SoftDeletable))
                {
                    var detedableEntity = p as SoftDeletable;
                    detedableEntity.IsDeleted = true;
                    _prandoDbContext.Update(detedableEntity);
                }
                else
                    _prandoDbContext.Remove(p);
            });
        }

        public List<T> FindAll()
        {
            return
            _prandoDbContext.Set<T>().ToList();
        }

        public List<T> FindAll(Expression<Func<T, bool>> func)
        {
            return
            _prandoDbContext.Set<T>().Where(func).ToList();
        }

        public T FindById(int id)
        {
            return
            _prandoDbContext.Set<T>().FirstOrDefault(p => p.Id == id);
        }

        public void Update(T t)
        {
            _prandoDbContext.Set<T>().Update(t);
        }

        public IQueryable<T> SqlStoredProcedure(string storedProcedureName ,params object[] parameters)
        {
            return
            _prandoDbContext.Set<T>()
                .FromSql(storedProcedureName, parameters);
        }
    }
}
