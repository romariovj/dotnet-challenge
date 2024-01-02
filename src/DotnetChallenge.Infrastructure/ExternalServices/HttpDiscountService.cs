using DotnetChallenge.Domain.Entities;
using DotnetChallenge.Domain.Interfaces;
using Newtonsoft.Json;

namespace DotnetChallenge.Infrastructure.ExternalServices
{
    public class HttpDiscountService : IDiscountService
    {
        private readonly HttpClient _httpClient;

        public HttpDiscountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> GetDiscountPercentageAsync(int productId)
        {
            string apiUrl = $"https://6593b8bd1493b0116068fdea.mockapi.io/api/v1/discounts/{productId}";

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string jsonContent = await response.Content.ReadAsStringAsync();
                DiscountApiResponse discountApiResponse = JsonConvert.DeserializeObject<DiscountApiResponse>(jsonContent);
                return discountApiResponse.DiscountPercentage;
            }

            throw new InvalidOperationException($"Error al obtener el porcentaje de descuento para el producto con ID {productId}");
        }
    }
}
