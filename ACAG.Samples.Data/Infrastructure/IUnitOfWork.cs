// ACAG.Samples.Data.Infrastructure
// *****************************************************************************************
//
// Name:        IUnitOfWork.cs
//
// Created:     28.01.2016 Kloon  
// Modified:    28.01.2016 Kloon    : Creation 
//
// *****************************************************************************************

namespace ACAG.Samples.Data.Infrastructure
{
    /// <summary>
    /// Interface as UnitOfWork pattern
    /// </summary>
    public interface IUnitOfWork
    {
        void Commit();
    }
}
