// ACAG.Samples.Data.Infrastructure
// *****************************************************************************************
//
// Name:        DbFactory.cs
//
// Created:     28.01.2016 Kloon  
// Modified:    28.01.2016 Kloon    : Creation 
//
// *****************************************************************************************

namespace ACAG.Samples.Data.Infrastructure
{
    /// <summary>
    /// Implementation for IDbFactory
    /// </summary>
    public class DbFactory : Disposable, IDbFactory
    {
        ACAGDataContext _dbContext;

        public ACAGDataContext Init()
        {
            return _dbContext ?? (_dbContext = new ACAGDataContext());
        }

        protected override void DisposeCore()
        {
            if (_dbContext != null)
                _dbContext.Dispose();
        }
    }
}
