using Core.Entity;
using Core.Factory;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dao
{
    public abstract class DaoFactory<TEntity, TPrimaryKey> : IFactory<TEntity, TPrimaryKey> where TEntity : Pessoa<TPrimaryKey>
    {
        protected ISession Session { get { return DaoUnitOfWork.Current.Session; } }

        public void Delete(TPrimaryKey id)
        {
            Session.Delete(Session.Load<TEntity>(id));
        }

        public TEntity Get(TPrimaryKey key)
        {
            return Session.Get<TEntity>(key);
        }

        public IQueryable<TEntity> GetAll()
        {
            return Session.Query<TEntity>();
        }

        public void Insert(TEntity entity)
        {
            Session.Save(entity);
        }

        public void Update(TEntity entity)
        {
            Session.Update(entity);
        }
    }
}
