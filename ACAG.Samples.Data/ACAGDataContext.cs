// ACAG.Samples.Data
// *****************************************************************************************
//
// Name:        ACAGDataContext.cs
//
// Created:     28.01.2016 Kloon  
// Modified:    28.01.2016 Kloon    : Creation 
//
// *****************************************************************************************

using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ACAG.Samples.Data.Configurations;
using ACAG.Samples.Entities;

namespace ACAG.Samples.Data
{

    /// <summary>
    /// Data Context
    /// </summary>
    public class ACAGDataContext : DbContext
    {
        /// <summary>
        /// Default constructor, initializes a new <see cref="ACAGDataContext"/>
        /// </summary>
        public ACAGDataContext()
            : base("ACAGDataContext")
        {
        }

        #region Entity Set
        /// <summary>
        /// Get and set Order 
        /// </summary>
        public IDbSet<Error> ErrorSet { get; set; }
        /// <summary>
        /// Get and set Order 
        /// </summary>
        public IDbSet<Order> OrderSet { get; set; }
        /// <summary>
        /// Get and set OrderPosition 
        /// </summary>
        public IDbSet<OrderPosition> OrderPositionSet { get; set; }

        #endregion

        public virtual void Commit()
        {
            base.SaveChanges();
        }
        /// <summary>
        /// Override OnModelCreating
        /// </summary>
        /// <param name="modelBuilder">Object DbModelBuilder</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            
            modelBuilder.Configurations.Add(new OrderConfiguration());
            modelBuilder.Configurations.Add(new OrderPositionConfiguration());
        }
    }
}
