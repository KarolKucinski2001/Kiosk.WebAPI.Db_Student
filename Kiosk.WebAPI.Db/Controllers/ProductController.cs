using Microsoft.AspNetCore.Mvc;
using Kiosk.WebAPI.Dto;
using Kiosk.WebAPI.Models;
using Kiosk.WebAPI.Persistance;
using Kiosk.WebAPI.Db.Services;

namespace Kiosk.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {

        private readonly IKioskUnitOfWork _unitOfWork;  
        private readonly IProductService _productService;   
        public ProductController(IKioskUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
        }
        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> Get()
        {
            var products = _unitOfWork.ProductRepository.GetAll()
                .ToList();

            List<ProductDto> result = new List<ProductDto>();
            foreach(var s in products)
            {
                result.Add(new ProductDto()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    UnitPrice = s.UnitPrice,
                });
            }
    
            return Ok(result);
        }

        [HttpGet("{id}", Name ="GetProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ProductDto> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var product = _unitOfWork.ProductRepository.Find(s => s.Id == id)
                .FirstOrDefault(s => s.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            var result = new ProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                UnitPrice = product.UnitPrice,
            };

            return Ok(result);
        }


        // return CreatedAtAction() - dynamicznie twrozony url
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create([FromBody] CreateProductDto dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            //var id = ProductStore.Products
            //    .Max(s => s.Id) + 1;

            var product = new Product()
            {
                //Id = id,
                Name = dto.Name,
                Description = dto.Description,
                UnitPrice = dto.UnitPrice,
                CreatedAt = DateTime.Now,
            };
            //ProductStore.Products.Add(product);
            _unitOfWork.ProductRepository.Insert(product);  
            

            var actionName = nameof(Get);
            var routeValues = new { id=product.Id };
            return CreatedAtAction(actionName, routeValues, null);
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id) 
        {
            var product = _unitOfWork.ProductRepository.Find(s => s.Id == id)   
                .FirstOrDefault(s => s.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            //ProductStore.Products.Remove(product);
            _unitOfWork.ProductRepository.Delete(product);
            
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update(int id, [FromBody] UpdateProductDto dto)
        {
            if ((dto == null) || (dto.Id == id))
            {
                return BadRequest();
            }

            var product = _unitOfWork.ProductRepository.Find(s => s.Id == dto.Id).FirstOrDefault(s => s.Id == dto.Id);
            if (product == null) 
            { 
                return NotFound();
            }

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.UnitPrice = dto.UnitPrice;

            return NoContent();
        }
    }
}
