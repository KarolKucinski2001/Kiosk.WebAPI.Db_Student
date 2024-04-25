using Microsoft.AspNetCore.Mvc;
using Kiosk.WebAPI.Dto;
using Kiosk.WebAPI.Models;
using Kiosk.WebAPI.Persistance;
using Kiosk.WebAPI.Db.Services;
using FluentValidation;

namespace Kiosk.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {

     //   private readonly IKioskUnitOfWork _unitOfWork;  
        private readonly IProductService _productService;
        private readonly IValidator<CreateProductDto> _validator;
        public ProductController(IProductService productService/*, IValidator<CreateProductDto> validator*/)
        {
            this._productService = productService;
            //_validator = validator;
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



        //----------------
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create([FromBody] CreateProductDto dto)
        {
            //var validationResult = _validator.Validate(dto);
            //if (!validationResult.IsValid)
            //{
            //    return BadRequest(validationResult);
            //}
            var id = ProductStore.Products.Max(s => s.Id) + 1;
            _productService.Create(dto);
            var actionName = nameof(Get);
            var routeValues = new { id };
            return CreatedAtAction(actionName, routeValues, null);
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
