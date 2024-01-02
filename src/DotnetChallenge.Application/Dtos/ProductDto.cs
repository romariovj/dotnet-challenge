namespace DotnetChallenge.Application.Dtos
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string StatusName { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal FinalPrice
        {
            get
            {
                return Price * (100 - (Discount == null ? 0:Discount )) / 100;
            }
        }
    }
}
