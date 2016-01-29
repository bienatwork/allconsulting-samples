using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ACAG.Samples.BusinessServices.Abstracts;
using ACAG.Samples.BusinessServices.Impls;
using ACAG.Samples.Data;
using ACAG.Samples.Data.Infrastructure;
using ACAG.Samples.Data.Repositories;
using Autofac;

namespace ACAG.Samples.Web
{
    public class Bootstrapper
    {
        /// <summary>
        /// Configures and builds Autofac IOC container.
        /// </summary>
        /// <returns></returns>
        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ACAGDataContext>().As<DbContext>().InstancePerRequest();

            // Register repositories and UnitOfWork
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<DbFactory>().As<IDbFactory>();

            builder.RegisterType<OrderRepository>().As<IOrderRepository>();
            builder.RegisterType<OrderPositionRepository>().As<IOrderPositionRepository>();

            // Register services
            builder.RegisterType<OrderBusinessService>().As<IOrderBusinessService>();
            builder.RegisterType<OrderPositionBusinessService>().As<IOrderPositionBusinessService>();

            // Build container
            return builder.Build();
        }
    }
}