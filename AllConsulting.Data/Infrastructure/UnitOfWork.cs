// Infrastructure
// *****************************************************************************************
//
// Name:		UnitOfWork.cs
//
// Created:		28.01.2016 ACAG  
// Modified:	28.01.2016 ACAG  	: Creation 
//
// *****************************************************************************************
namespace ACAG.Data.Infrastructure
{
    /// <summary>
    /// this class implement IUnitOfWork
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory _dbFactory;
        private ACAGDataContext _dbContext;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this._dbFactory = dbFactory;
        }

        public ACAGDataContext DbContext
        {
            get { return _dbContext ?? (_dbContext = _dbFactory.Init()); }
        }

        public void Commit()
        {
            DbContext.Commit();
        }
    }
}
