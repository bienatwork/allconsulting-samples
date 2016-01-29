// ACAG.Samples.Entities
// *****************************************************************************************
//
// Name:        IEntityBase.cs
//
// Created:     28.01.2016 Kloon  
// Modified:    28.01.2016 Kloon    : Creation 
//
// *****************************************************************************************

namespace ACAG.Samples.Entities
{
    /// <summary>
    /// Interface that must be implemented by all entity
    /// </summary>
    public interface IEntityBase
    {
        /// <summary>
        ///  The identifier of entity
        /// </summary>
        int ID { get; set; }
    }
}
