using Microsoft.AspNetCore.Mvc;
using Kiosk.WebAPI.Dto;
using Kiosk.WebAPI.Models;
using Kiosk.WebAPI.Persistance;
using Kiosk.WebAPI.Db.Services;

namespace Kiosk.WebAPI.Controllers
{
   // [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {

        private readonly IKioskUnitOfWork _unitOfWork;  
        private readonly IProductService _productService;   
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> Get()
        {
            var products = _productService.GetAll();
            return Ok(products);
        }

        [HttpGet("{id}", Name ="GetProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ProductDto> Get(int id)
        {
            try
            {
                var product = _productService.GetById(id);
                return Ok(product);
            }
            catch (ArgumentException)
            { 
                return NotFound();
            }
        }


        // return CreatedAtAction() - dynamicznie twrozony url
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create([FromBody] CreateProductDto dto)
        {
            if(dto == null)
            {
                return BadRequest(); 
            }
            try
            {
                var id = ProductStore.Products.Max(s => s.Id) + 1;
                _productService.Create(dto);
                var actionName = nameof(Create);
                return CreatedAtAction(actionName, new { id }, null);
            }
            catch(Exception)
            {
                return BadRequest();    
            }
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id) 
        {
            try
            {
                _productService.Delete(id);
                return Ok();
            }
            catch(Exception)
            {
                return NotFound();    
            }
            
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update(int id, [FromBody] UpdateProductDto dto)
        {
            if(dto==null||dto.Id!=id) { return BadRequest(); } //BadRequest - wina jest po stronie klienta
            try
            {
               _productService.Update(dto);
                return Ok();
            }
            catch (Exception)
            { 
                return NotFound(); 
            }
        }
    }
}
