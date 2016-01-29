// ACAG.Samples.Data.Infrastructure
// *****************************************************************************************
//
// Name:        IDbFactory.cs
//
// Created:     28.01.2016 Kloon  
// Modified:    28.01.2016 Kloon    : Creation 
//
// *****************************************************************************************

using System;

namespace ACAG.Samples.Data.Infrastructure
{
    /// <summary>
    /// Define a database factory who takes responsibility for creating database context and initializing if needed
    /// </summary>
    public interface IDbFactory : IDisposable
    {
        /// <summary>
        /// Initilize a database context with connection string declared in Web.config
        /// </summary>
        /// <returns></returns>
        ACAGDataContext Init();
    }
}
