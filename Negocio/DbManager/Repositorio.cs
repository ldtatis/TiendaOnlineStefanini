using System.Data.Entity;
using System.Linq;

namespace Negocio.DbManager
{
    public class Repositorio<TEntity> : IRepositorio<TEntity> where TEntity : class, new()
    {
        protected DbContext _entities;
        protected DbSet<TEntity> _dbSet;

        public Repositorio(DbContext entities)
        {
            this._entities = entities;
            this._dbSet = entities.Set<TEntity>();
        }
        public IQueryable<TEntity> All
        {
            get { return _dbSet; }
        }

        public virtual TEntity Insert(TEntity entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public virtual TEntity Delete(TEntity entity)
        {
            _dbSet.Attach(entity);
            _dbSet.Remove(entity);
            return entity;
        }

        public virtual TEntity Update(TEntity editedEntity, TEntity originalEntity, out bool changed)
        {
            _entities.Entry(originalEntity).CurrentValues.SetValues(editedEntity);
            changed = _entities.Entry(originalEntity).State == EntityState.Modified;
            return originalEntity;

        }
        public void Commit()
        {
            _entities.SaveChanges();
        }

    }
}
