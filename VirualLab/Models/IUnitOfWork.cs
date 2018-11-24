using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirualLab.Models
{
    interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>();
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
