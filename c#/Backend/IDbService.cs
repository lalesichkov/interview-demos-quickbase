using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Backend
{
    public interface IDbService
    {
        public Task<List<Country>> GetPopulationAsync();
    }
}
