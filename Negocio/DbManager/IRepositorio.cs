using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.DbManager
{
    public interface IRepositorio<T>
    {
        IQueryable<T> All { get; }
        T Delete(T entity);
        T Insert(T entity);
        T Update(T entity, T oldEntity, out bool modified);
        void Commit();
    }
}
