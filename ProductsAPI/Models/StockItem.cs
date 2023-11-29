namespace ProductsAPI.Models
{
    public class StockItem
    {
        public Guid Id { get; set; }
        public Guid Id_Store { get; set; }
        public Guid Id_Product { get; set; }
        public Guid ProductQuantity { get; set; }
    }
}
