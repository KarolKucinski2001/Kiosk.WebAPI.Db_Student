using Kiosk.WebAPI.Models;

namespace Kiosk.WebAPI.Persistance
{
    // interfejsy repozytoriów specyficznych
    public interface IProductRepository : IRepository<Product>
    {
        int GetMaxId();
        //bool UniqueName(string name);
    }
}
