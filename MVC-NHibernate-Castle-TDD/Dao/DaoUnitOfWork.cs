using Core.Factory;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dao
{
    public class DaoUnitOfWork : IUnitOfWork
    {
        [ThreadStatic]
        private static DaoUnitOfWork _current;

        public static DaoUnitOfWork Current
        {
            get { return _current; }
            set { _current = value; }
        }

        public ISession Session { get; private set; }

        public readonly ISessionFactory _sessionFactory;
        
        public DaoUnitOfWork(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        private ITransaction _transaction;

        public void BeginTransaction()
        {
            Session = _sessionFactory.OpenSession();
            _transaction = Session.BeginTransaction();
        }

        public void Commit()
        {
            try { _transaction.Commit(); }
            finally { Session.Close(); }
        }

        public void Rollback()
        {
            try { _transaction.Rollback(); }
            finally { Session.Close(); }
        }
    }
}
