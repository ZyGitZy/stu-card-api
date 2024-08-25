using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace stu_card_entity_store.Store
{
    public class EntityStore<T> : IEntityStore<T> where T : CommonEntity, new()
    {
        readonly DbContext dbContext;
        readonly DbSet<T> dbSet;
        public EntityStore(DbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<T>();
        }

        public T Create(T entity)
        {
            dbSet.Add(entity);
            return entity;
        }

        public async Task<T> CreateAsync(T entity)
        {
            Create(entity);

            await this.SaveChangeAsync();

            return entity;
        }

        public void Delete(T entity)
        {
            if (entity is not IDeleteStore entityLogicDelete)
            {
                dbSet.Remove(entity);
            }
            else
            {
                entityLogicDelete.IsDeleted = true;
                dbSet.Update(entity);
            }
        }

        public async Task DeleteAsync(T entity)
        {
            this.Delete(entity);
            await this.SaveChangeAsync();
        }

        public async Task<T?> FindAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> fun)
        {
            if (fun == null)
            {
                return await this.dbSet.CountAsync();
            }

            return await this.dbSet.CountAsync(fun);
        }

        public IQueryable<T> Query(Expression<Func<T, bool>>? fun)
        {
            if (fun != null)
            {
                return dbSet.Where(fun);
            }

            return dbSet;
        }

        public async Task<int> SaveChangeAsync()
        {
            return await this.dbContext.SaveChangesAsync();
        }

        public void Update(T entity)
        {

        }

        public async Task UpdateAsync(T entity)
        {
            await this.SaveChangeAsync();
        }
    }
}
