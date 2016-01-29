using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACAG.Samples.Entities;

namespace ACAG.Samples.Data.Repositories
{
    public interface IOrderRepository : IEntityBaseRepository<Order>
    {
        IEnumerable<Order> Find(string sort, string searchFullOrder);
    }
}
