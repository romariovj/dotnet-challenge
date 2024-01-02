namespace DotnetChallenge.Domain.Interfaces
{
    public interface IDiscountService
    {
        Task<int> GetDiscountPercentageAsync(int productId);
    }
}
