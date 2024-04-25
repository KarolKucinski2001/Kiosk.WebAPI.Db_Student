using Kiosk.WebAPI.Dto;

namespace Kiosk.WebAPI.Db.Services
{
    public interface IProductService
    {
        List<ProductDto> GetAll();
        ProductDto GetById(int id);
        void Create(CreateProductDto dto);
        void Update(UpdateProductDto dto);
        void Delete(int id);
        bool IsInUse(string name);
    }
}
