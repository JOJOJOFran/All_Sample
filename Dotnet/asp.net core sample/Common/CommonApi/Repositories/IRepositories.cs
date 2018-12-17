using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommonApi.Repositories
{
    public interface IRepositories
    {
        void Add();
        void Delete();
        void Update();
        void Select();
    }
}
