namespace DotnetChallenge.Application.Services
{
    public interface IProductStatusCacheService
    {
        Dictionary<int, string> GetCachedProductStatus();
    }
}
