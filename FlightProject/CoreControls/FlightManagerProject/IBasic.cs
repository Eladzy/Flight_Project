using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagerProject
{
    public interface IBasic<T> where T : IPoco
    {
        T Get(long id);
        IList<T> GetAll();
        void Add(T t);
        void Remove(T t);
        void Update(T t);
    }
}
