namespace AllConsulting.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        AllConsultingDataContext _dbContext;

        public AllConsultingDataContext Init()
        {
            return _dbContext ?? (_dbContext = new AllConsultingDataContext());
        }

        protected override void DisposeCore()
        {
            if (_dbContext != null)
                _dbContext.Dispose();
        }
    }
}
