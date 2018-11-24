using System.Linq;

namespace VirualLab.Models
{
    class EtalonRepository : IRepository<Etalon>
    {
        private ApplicationDbContext _dbContext;
        public EtalonRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(Etalon Obj)
        {
            var etalon = _dbContext.Etalons.Create();
            etalon = Obj;
        }
        public Etalon Get(int Id)
        {
            return _dbContext.Etalons.Find(Id);
        }
        public Etalon[] Get()
        {
            return _dbContext.Etalons.ToArray();
        }
        public void Update(Etalon Obj)
        {
            var etalon = _dbContext.Etalons.Find(Obj.Id);
            etalon = Obj;
        }
        public void Delete(Etalon Obj)
        {
            _dbContext.Etalons.Remove(Obj);
        }
    }
}
