// ACAG.Samples.Web
// *****************************************************************************************
//
// Name:        AutofacWebapiConfig.cs
//
// Created:     28.01.2016 Kloon  
// Modified:    28.01.2016 Kloon    : Creation 
//
// *****************************************************************************************

using System.Data.Entity;
using System.Reflection;
using System.Web.Http;
using ACAG.Samples.Data;
using ACAG.Samples.Data.Infrastructure;
using ACAG.Samples.Data.Repositories;
using Autofac;
using Autofac.Integration.WebApi;

namespace ACAG.Samples.Web
{
    /// <summary>
    /// A bootrapper for Inversion of Container, using Autofac third-party library
    /// </summary>
    public class AutofacWebapiConfig
    {
        public static IContainer Container;
        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // EF DataContext
            builder.RegisterType<ACAGDataContext>()
                   .As<DbContext>()
                   .InstancePerRequest();

            builder.RegisterType<DbFactory>()
                .As<IDbFactory>()
                .InstancePerRequest();

            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerRequest();

            builder.RegisterGeneric(typeof(EntityBaseRepository<>))
                   .As(typeof(IEntityBaseRepository<>))
                   .InstancePerRequest();

            // Services

            Container = builder.Build();

            return Container;
        }
    }
}