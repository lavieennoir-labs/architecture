using System.Linq;

namespace VirualLab.Models
{
    class TasksRepository : IRepository<Task>
    {
        private ApplicationDbContext _dbContext;
        public TasksRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(Task Obj)
        {
            var task = _dbContext.Tasks.Create();
            task = Obj;
        }
        public Task Get(int Id)
        {
            return _dbContext.Tasks.Find(Id);
        }
        public Task[] Get()
        {
            return _dbContext.Tasks.ToArray();
        }
        public void Update(Task Obj)
        {
            var task = _dbContext.Tasks.Find(Obj.Id);
            task = Obj;
        }
        public void Delete(Task Obj)
        {
            _dbContext.Tasks.Remove(Obj);
        }
    }
}
