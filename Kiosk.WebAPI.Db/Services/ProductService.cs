using Kiosk.WebAPI.Db.Exceptions;
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

        public void Create(CreateProductDto dto)
        {

            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                UnitPrice = dto.UnitPrice,
                CreatedAt = DateTime.Now
            };

            _unitOfWork.ProductRepository.Insert(product);
            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            var product = _unitOfWork.ProductRepository.Get(id);
            if(product==null) 
            {
                throw new BadRequestException("Product not found 123");
            }
            _unitOfWork.ProductRepository.Delete(product);
            _unitOfWork.Commit();
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
            if(id<0)
            {
                throw new BadRequestException("Id nie może być ujemne");
            }

            var product=_unitOfWork.ProductRepository.Get(id);
            if(product==null) 
            { 
                throw new NotFoundException("Product not found");
            }
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
            var product= _unitOfWork.ProductRepository.Get(dto.Id);
            if (product == null)
            { 
                throw new NotFoundException("Product not found");
            }
            
            product.UnitPrice = dto.UnitPrice;
            product.Name = dto.Name;
            product.Description = dto.Description;
            _unitOfWork.Commit();
        }
    }
}
