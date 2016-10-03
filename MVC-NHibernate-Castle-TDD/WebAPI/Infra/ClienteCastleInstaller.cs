using Core.Service;
using Dao;
using Dao.Entity;
using NHibernate;
using System.Configuration;
using System.Reflection;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

namespace WebAPI.Infra
{
    public class ClienteCastleInstaller : IWindsorInstaller
    {

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Kernel.ComponentRegistered += Kernel_ComponentRegistered;

            container.Register(

                //Nhibernate session factory
                Component.For<ISessionFactory>().UsingFactoryMethod(CreateNhSessionFactory).LifeStyle.Singleton,

                //Unitofwork interceptor
                Component.For<CastleUoWInterceptor>().LifeStyle.Transient,

                Classes.FromAssembly(Assembly.GetAssembly(typeof(DaoClienteFactory))).InSameNamespaceAs<DaoClienteFactory>().WithService.DefaultInterfaces().LifestyleTransient(),

                Classes.FromAssembly(Assembly.GetAssembly(typeof(ClienteService))).InSameNamespaceAs<ClienteService>().WithService.DefaultInterfaces().LifestyleTransient()

                );
        }

        private static ISessionFactory CreateNhSessionFactory()
        {
            var connStr = ConfigurationManager.ConnectionStrings["stringConnection"].ConnectionString;
            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connStr))
                .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetAssembly(typeof(ClienteMapping))))
                .BuildSessionFactory();
        }

        void Kernel_ComponentRegistered(string key, Castle.MicroKernel.IHandler handler)
        {
            if (UoWHelper.IsRepositoryClass(handler.ComponentModel.Implementation))
                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(CastleUoWInterceptor)));
            

            foreach (var method in handler.ComponentModel.Implementation.GetMethods())
            {
                if (UoWHelper.HasUnitOfWorkAttribute(method))
                {
                    handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(CastleUoWInterceptor)));
                    return;
                }
            }
        }

    }
}