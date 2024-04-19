using System.ComponentModel.DataAnnotations;

namespace Kiosk.WebAPI.Dto
{
    public class CreateProductDto
    {
        [MinLength(2)]
        [MaxLength(20)]    
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Unit price must be greater than 1 grosz.")]
        public decimal UnitPrice { get; set; }
    }
}
