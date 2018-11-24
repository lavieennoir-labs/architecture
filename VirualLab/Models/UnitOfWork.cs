namespace VirualLab.Models
{
    class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _dbContext;
        private bool UsingTransaction = false;

        public IRepository<T> GetRepository<T>()
        {
            switch ((typeof(T)).Name)
            {
                case "Task":
                    return new TasksRepository(_dbContext ?? new ApplicationDbContext()) as IRepository<T>;
                case "Etalon":
                    return new EtalonRepository(_dbContext ?? new ApplicationDbContext()) as IRepository<T>;
                default:
                    return null;
            }
        }
        public void SaveChanges()
        {
            _dbContext?.SaveChanges();
        }
        public void BeginTransaction()
        {
            _dbContext = new ApplicationDbContext();
            UsingTransaction = true;
        }
        public void CommitTransaction()
        {
            _dbContext.SaveChanges();
            _dbContext.Dispose();
            _dbContext = null;
            UsingTransaction = false;
        }
        public void RollbackTransaction()
        {
            _dbContext.Dispose();
            _dbContext = null;
            UsingTransaction = false;
        }
    }
}
