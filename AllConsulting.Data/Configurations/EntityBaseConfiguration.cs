// Configurations
// *****************************************************************************************
//
// Name:		EntityBaseConfiguration.cs
//
// Created:		28.01.2016 ACAG  
// Modified:	28.01.2016 ACAG  	: Creation 
//
// *****************************************************************************************

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration; 
using ACAG.Entities;

namespace ACAG.Data.Configurations
{
    /// <summary>
    /// this is class EntityBaseConfiguration
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityBaseConfiguration<T> : EntityTypeConfiguration<T> where T : class, IEntityBase
    {
        public EntityBaseConfiguration()
        {
            HasKey(e => e.ID);
            Property(x => x.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
