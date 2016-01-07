using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Security.Cryptography;
using System.Text;
using AllConsulting.Entities;

namespace AllConsulting.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<AllConsultingDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AllConsultingDataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //IList orderList = new List<Order>();
            //for (int index = 0; index < 100; index++)
            //{
            //    IList<OrderPosition> orderPositionList = new List<OrderPosition>();
            //    for (int i = 0; i < 15; i++)
            //    {
            //        var orderPos = new OrderPosition
            //        {
            //            PositionNumber = index * i + 1,
            //            Pieces = index * i + 1,
            //            Text = string.Format("Pieces - {0:D5}", index * i + 1),
            //            Price = 1.5,
            //            Total = (index * i + 1) * 1.5
            //        };

            //        orderPositionList.Add(orderPos);
            //    }

            //    var order = new Order
            //    {
            //        CustomerNumber = string.Format("{0:D5}", index + 1),
            //        OrderPositions = orderPositionList,
            //    };

            //    orderList.Add(order);
            //}

            //foreach (Order order in orderList)
            //{
            //    context.OrderSet.AddOrUpdate(x => x.CustomerNumber, order);
            //}
            //context.SaveChanges();

        }
    }
}
