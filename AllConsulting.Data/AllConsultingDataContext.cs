using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using AllConsulting.Data.Configurations;
using AllConsulting.Entities;

namespace AllConsulting.Data
{
    public class AllConsultingDataContext : DbContext
    {
        public AllConsultingDataContext()
            : base("AllConsultingDataContext")
        {
        }

        #region Entity Set
        
        public IDbSet<Error> ErrorSet { get; set; }

        public IDbSet<Order> OrderSet { get; set; }
        public IDbSet<OrderPosition> OrderPositionSet { get; set; }

        #endregion

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            
            modelBuilder.Configurations.Add(new OrderConfiguration());
            modelBuilder.Configurations.Add(new OrderPositionConfiguration());
        }
    }
}
