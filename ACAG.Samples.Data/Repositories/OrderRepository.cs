using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACAG.Samples.Data.Infrastructure;
using ACAG.Samples.Entities;

namespace ACAG.Samples.Data.Repositories
{
    public class OrderRepository : EntityBaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }

        public IEnumerable<Order> Find(string sort, string searchFullOrder)
        {
            var orders = DbContext.OrderSet.OrderByDescending(x=>x.ID).Where(
                        x =>
                            x.CustomerNumber.Contains(searchFullOrder) ||
                            (SqlFunctions.DateName("dd", x.OrderDate) + "/" + SqlFunctions.DatePart("mm", x.OrderDate) + "/" + SqlFunctions.DateName("yyyy", x.OrderDate)).Contains(searchFullOrder) ||
                            (SqlFunctions.DateName("dd", x.DeliveryDate) + "/" + SqlFunctions.DatePart("mm", x.DeliveryDate) + "/" + SqlFunctions.DateName("yyyy", x.DeliveryDate)).Contains(searchFullOrder) ||
                            SqlFunctions.StringConvert(x.TotalPrice).Contains(searchFullOrder));

            switch (sort.ToLower())
            {
                case "customernumber desc":
                    orders = orders.OrderByDescending(x => x.CustomerNumber);
                    break;
                case "customernumber asc":
                    orders = orders.OrderBy(x => x.CustomerNumber);
                    break;
                case "deliverydate desc":
                    orders = orders.OrderByDescending(x => x.DeliveryDate);
                    break;
                case "deliverydate asc":
                    orders = orders.OrderBy(x => x.DeliveryDate);
                    break;
                case "orderdate desc":
                    orders = orders.OrderByDescending(x => x.OrderDate);
                    break;
                case "orderdate asc":
                    orders = orders.OrderBy(x => x.OrderDate);
                    break;
                case "totalprice desc":
                    orders = orders.OrderByDescending(x => x.TotalPrice);
                    break;
                case "totalprice asc":
                    orders = orders.OrderBy(x => x.TotalPrice);
                    break;
            }
            return orders;
        }
    }
}
