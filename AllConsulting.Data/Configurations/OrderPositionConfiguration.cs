using AllConsulting.Entities;

namespace AllConsulting.Data.Configurations
{
    public class OrderPositionConfiguration : EntityBaseConfiguration<OrderPosition>
    {
        public OrderPositionConfiguration()
        {
            Property(x => x.PositionNumber).IsRequired();
            //Property(x => x.Pieces).IsRequired();
            Property(x => x.Text).IsRequired();
            Property(x => x.Price).IsRequired();
            Property(x => x.Total).IsRequired();
        }
    }
}
