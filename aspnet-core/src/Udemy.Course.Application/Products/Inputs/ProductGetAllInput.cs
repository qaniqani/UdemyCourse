namespace Udemy.Course.Products.Inputs
{
    public class ProductGetAllInput
    {
        public int? ProductId { get; set; }
        public string Name { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int SkipCount { get; set; } = 0;
        public int MaxResultCount { get; set; } = 20;
        public string Sorting { get; set; }
    }
}