// Infrastructure
// *****************************************************************************************
//
// Name:		IDbFactory.cs
//
// Created:		28.01.2016 ACAG  
// Modified:	28.01.2016 ACAG  	: Creation 
//
// *****************************************************************************************
using System; 
namespace ACAG.Data.Infrastructure
{
    /// <summary>
    /// Interface IDbFactory
    /// </summary>
    public interface IDbFactory : IDisposable
    {
        ACAGDataContext Init();
    }
}
