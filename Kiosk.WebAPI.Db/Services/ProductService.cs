using Kiosk.WebAPI.Dto;
using Kiosk.WebAPI.Models;
using Kiosk.WebAPI.Persistance;

namespace Kiosk.WebAPI.Db.Services
{

    public class ProductService:IProductService
    {
        private readonly IKioskUnitOfWork _unitOfWork;
        public ProductService(IKioskUnitOfWork iuow)
        {
            _unitOfWork = iuow; 
        }

        public int Create(CreateProductDto dto)
        {

            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                UnitPrice = dto.UnitPrice,
                CreatedAt = DateTime.Now,

            };
            _unitOfWork.ProductRepository.Insert(product);

            return product.Id;
        }

        public void Delete(int id)
        {
            var product = _unitOfWork.ProductRepository.Find(a => a.Id == id).FirstOrDefault(a=>a.Id==id);
            if(product!=null) { throw new ArgumentException("Product not found"); }
            _unitOfWork.ProductRepository.Delete(product);
        }

        public List<ProductDto> GetAll()
        {
            var products=_unitOfWork.ProductRepository.GetAll();
            return products.Select(p=> new ProductDto
            {
                Id=p.Id,
                Name = p.Name,  
                Description = p.Description,
                UnitPrice = p.UnitPrice
            }
            ).ToList();
        }

        public ProductDto GetById(int id)
        {
            var product=_unitOfWork.ProductRepository.Find(a =>a.Id==id).FirstOrDefault();
            if(product!=null) { throw new ArgumentException("Product not found"); }
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description=product.Description,
                UnitPrice = product.UnitPrice,
                
            };
        }

        public void Update(UpdateProductDto dto)
        {
            var product= _unitOfWork.ProductRepository.Find(a=>a.Id==dto.Id).FirstOrDefault();
            if (product == null) { throw new ArgumentException("Product not found"); }
            
            product.UnitPrice = dto.UnitPrice;
            product.Name = dto.Name;
            product.Description = dto.Description;

            
        }
    }
}
