using AutoMapper;
using DotnetChallenge.Domain.Repositories;
using DotnetChallenge.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DotnetChallenge.Infrastructure.Configurations
{
    public abstract class CrudRepository<TDomain, TDatabase, ID> : ICrudRepository<TDomain, ID> 
        where TDomain : class
        where TDatabase : class
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        protected CrudRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<TDomain>> FindAllAsync()
        {
            var entities = await _context.Set<TDatabase>().AsNoTracking().ToListAsync();
            return _mapper.Map<IReadOnlyList<TDomain>>(entities);
        }

        public async Task<TDomain?> FindByIdAsync(ID id)
        {
            var entity = await _context.Set<TDatabase>().FindAsync(id);
            return _mapper?.Map<TDomain>(entity);
        }

        public async Task<TDomain?> SaveAsync(TDomain entityDomain)
        {
            var entityDb = _mapper.Map<TDatabase>(entityDomain);


            EntityState entityState = _context.Entry(entityDb).State;

            _ = entityState switch
            {
                EntityState.Detached => _context.Set<TDatabase>().Add(entityDb),
                EntityState.Modified => _context.Set<TDatabase>().Update(entityDb),
            };

            await _context.SaveChangesAsync();
            return entityDomain;
        } 
    }

}
