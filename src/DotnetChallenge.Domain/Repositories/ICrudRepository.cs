using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetChallenge.Domain.Repositories
{
    public interface ICrudRepository<TDomain, ID>
    {
        Task<IReadOnlyList<TDomain>> FindAllAsync();
        Task<TDomain?> FindByIdAsync(ID id);
        Task<TDomain?> SaveAsync(TDomain entity);
    }
}
