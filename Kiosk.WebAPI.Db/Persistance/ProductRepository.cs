using Kiosk.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.WebAPI.Persistance
{
    // Implementacja repozytoriów specyficznych

    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly KioskDbContext _kioskDbContext;

        public ProductRepository(KioskDbContext context)
            : base(context)
        {
            _kioskDbContext = context;
        }

        public int GetMaxId()
        {
            return _kioskDbContext.Products.Max(x => x.Id);
        }


    }

    
}
