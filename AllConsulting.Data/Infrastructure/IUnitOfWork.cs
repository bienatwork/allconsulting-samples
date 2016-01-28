// Infrastructure
// *****************************************************************************************
//
// Name:		IUnitOfWork.cs
//
// Created:		28.01.2016 ACAG  
// Modified:	28.01.2016 ACAG  	: Creation 
//
// *****************************************************************************************

namespace ACAG.Data.Infrastructure
{
    /// <summary>
    /// Definition IUnitOfWork
    /// </summary>
    public interface IUnitOfWork
    {
        void Commit();
    }
}
