using AllConsulting.Entities;

namespace AllConsulting.Data.Configurations
{
    public class OrderConfiguration : EntityBaseConfiguration<Order>
    {
        public OrderConfiguration()
        {
            Property(x => x.CustomerNumber).IsRequired();//.HasMaxLength(500);
            Property(x => x.OrderDate).IsOptional();
            Property(x => x.DeliveryDate).IsOptional();
            Property(x => x.TotalPrice).IsOptional();

            HasMany(x => x.OrderPositions).WithRequired(y => y.Order).HasForeignKey(y => y.OrderID);
        }
    }
}
