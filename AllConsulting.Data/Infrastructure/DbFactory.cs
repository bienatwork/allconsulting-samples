// Infrastructure
// *****************************************************************************************
//
// Name:		DbFactory.cs
//
// Created:		28.01.2016 ACAG  
// Modified:	28.01.2016 ACAG  	: Creation 
//
// *****************************************************************************************
namespace ACAG.Data.Infrastructure
{
    /// <summary>
    /// DbFactory
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
