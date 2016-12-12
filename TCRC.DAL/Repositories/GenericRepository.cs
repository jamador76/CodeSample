using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace DAL.Repositories
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        #region Members
        internal TCRCEntities context;
        internal DbSet<TEntity> dbSet;
        #endregion

        #region Methods

        /// <summary>
        /// Generic repository constructor
        /// </summary>
        /// <param name="context">The database context</param>
        public GenericRepository(TCRCEntities context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        /// <summary>
        /// Gets the entities from the repository
        /// </summary>
        /// <param name="filter">The filter</param>
        /// <param name="orderBy">The order by clause</param>
        /// <param name="includeProperties"></param>
        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        /// <summary>
        /// Gets entity by id
        /// </summary>
        /// <param name="id">The entity id</param>
        /// <returns></returns>
        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        /// <summary>
        /// Inserts an entity
        /// </summary>
        /// <param name="entity">The entity to insert</param>
        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        public virtual void Delete(TEntity entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        /// <summary>
        /// Delete an entity by id
        /// </summary>
        /// <param name="id">The id of the entity to delete</param>
        public virtual void Delete(object id)
        {
            TEntity entity = dbSet.Find(id);
            Delete(entity);
        }

        /// <summary>
        /// Updates an entity
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(TEntity entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Reloads an entity
        /// </summary>
        /// <param name="entity">The entity to reload</param>
        public virtual void Reload(TEntity entity)
        {
            context.Entry(entity).Reload();
        }
        #endregion
    }
}