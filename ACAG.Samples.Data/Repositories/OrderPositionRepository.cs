using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACAG.Samples.Data.Infrastructure;
using ACAG.Samples.Entities;

namespace ACAG.Samples.Data.Repositories
{
    public class OrderPositionRepository : EntityBaseRepository<OrderPosition>, IOrderPositionRepository
    {
        public OrderPositionRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }
    }
}
