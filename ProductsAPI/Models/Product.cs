namespace ProductsAPI.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public double Price { get; set; } = 0.00;
        public int QuantityStocked { get; set; }
    }
}


