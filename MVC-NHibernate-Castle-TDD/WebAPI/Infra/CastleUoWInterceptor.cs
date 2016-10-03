using NHibernate;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using Dao;

namespace WebAPI.Infra
{
    public class CastleUoWInterceptor : Castle.DynamicProxy.IInterceptor
    {
        private readonly ISessionFactory _sessionFactory;

        public CastleUoWInterceptor(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public void Intercept(IInvocation invocation)
        {
            if (DaoUnitOfWork.Current != null || !RequiresDbConnection(invocation.MethodInvocationTarget))
            {
                invocation.Proceed();
                return;
            }

            try
            {
                DaoUnitOfWork.Current = new DaoUnitOfWork(_sessionFactory);
                DaoUnitOfWork.Current.BeginTransaction();

                try
                {
                    invocation.Proceed();
                    DaoUnitOfWork.Current.Commit();
                }
                catch
                {
                    try { DaoUnitOfWork.Current.Rollback(); }
                    catch { } throw;
                }
            }
            finally { DaoUnitOfWork.Current = null; }
        }

        private static bool RequiresDbConnection(MethodInfo methodInfo)
        {
            if (UoWHelper.HasUnitOfWorkAttribute(methodInfo))
                return true;
            
            if (UoWHelper.IsRepositoryMethod(methodInfo))
                return true;

            return false;
        }
    }
}