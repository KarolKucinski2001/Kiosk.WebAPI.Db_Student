using Kiosk.WebAPI.Models;

namespace Kiosk.WebAPI.Persistance
{
    public class ProductStore
    {
        public static List<Product> Products = new List<Product>()
        {
            new Product() { Id = 1, Name = "Kawa", Description = "Mała czarna", UnitPrice = 12, CreatedAt = DateTime.Now.AddDays(-5)},
            new Product() { Id = 2, Name = "Herbata", Description = "English Tea", UnitPrice = 8, CreatedAt = DateTime.Now.AddYears(-3)},
        };
    }
}
