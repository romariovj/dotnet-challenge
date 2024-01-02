using Microsoft.Extensions.Caching.Memory;

namespace DotnetChallenge.Application.Services
{
    public class ProductStatusCacheService : IProductStatusCacheService
    {
        private readonly IMemoryCache _cache;
        private readonly string _cacheKey = "ProductStatusCache";
        private readonly int _minutesInCache = 5;

        public ProductStatusCacheService(IMemoryCache cache)
        {
            _cache = cache;
        }


        public Dictionary<int, string> GetCachedProductStatus()
        {
            if (!_cache.TryGetValue(_cacheKey, out Dictionary<int, string> productStatusDictionary))
            {
                productStatusDictionary = CreateProductStatusDictionary();

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_minutesInCache)
                };

                _cache.Set(_cacheKey, productStatusDictionary, cacheEntryOptions);
            }

            return productStatusDictionary;
        }

        private Dictionary<int, string> CreateProductStatusDictionary()
        {
            // Puedes modificar esto según tus necesidades
            return new Dictionary<int, string>
            {
                { 1, "Active" },
                { 0, "Inactive" }
            };
        }

    }
}
