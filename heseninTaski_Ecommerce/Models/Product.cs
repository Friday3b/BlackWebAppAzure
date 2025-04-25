namespace heseninTaski_Ecommerce.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Slug { get; set; }
    public decimal Price { get; set; }
    public string CreatedBy { get; set; }
}
