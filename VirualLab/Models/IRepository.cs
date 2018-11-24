using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirualLab.Models
{
    interface IRepository<T>
    {
        void Create(T Obj);
        T Get(int Id);
        T[] Get();
        void Update(T Obj);
        void Delete(T Obj);
    }
}
