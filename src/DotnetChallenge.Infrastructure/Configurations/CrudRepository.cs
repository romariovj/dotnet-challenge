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

        public async Task<TDomain?> CreateAsync(TDomain entityDomain)
        {
            var entityDb = _mapper.Map<TDatabase>(entityDomain);
            _context.Set<TDatabase>().Add(entityDb);
            await _context.SaveChangesAsync();
            return _mapper?.Map<TDomain>(entityDomain);
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

        public async Task<TDomain?> UpdateAsync(TDomain entityDomain)
        {
            var entityDb = _mapper.Map<TDatabase>(entityDomain);
            var existingDatabaseEntity = await _context.Set<TDatabase>().FindAsync(GetId(entityDb));

            if (existingDatabaseEntity != null)
            {
                _context.Entry(existingDatabaseEntity).CurrentValues.SetValues(entityDb);
                await _context.SaveChangesAsync();
            }

            return _mapper.Map<TDomain>(existingDatabaseEntity);

        }

        private int GetId(TDatabase entity)
        {
            var propertyInfo = typeof(TDatabase).GetProperty("Id");
            return (int)propertyInfo.GetValue(entity);
        }

    }

}
