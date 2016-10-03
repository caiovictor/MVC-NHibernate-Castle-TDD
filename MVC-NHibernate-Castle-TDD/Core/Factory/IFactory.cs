using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Factory
{
    public interface IFactory
    {

    }

    public interface IFactory<TEntity, TPrimaryKey> : IFactory where TEntity : Pessoa<TPrimaryKey>
    {
        IQueryable<TEntity> GetAll();

        TEntity Get(TPrimaryKey key);

        void Insert(TEntity entity);

        void Update(TEntity entity);

        void Delete(TPrimaryKey id);
    }
}
