using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace stu_card_entity_store.Store
{
    public interface IEntityStore<T>
    {
        IQueryable<T> Query(Expression<Func<T, bool>>? fun = null);
        Task<T?> FindAsync(int id);
        void Delete(T entity);
        Task DeleteAsync(T entity);
        void Update(T entity);
        Task UpdateAsync(T entity);
        T Create(T entity);

        Task<T> CreateAsync(T entity);
        Task<int> CountAsync(Expression<Func<T, bool>>? fun = null);
        Task<int> SaveChangeAsync();
    }
}
