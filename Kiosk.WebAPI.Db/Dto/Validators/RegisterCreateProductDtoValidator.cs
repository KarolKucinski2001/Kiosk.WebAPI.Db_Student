using FluentValidation;
using Kiosk.WebAPI.Db.Services;
using Kiosk.WebAPI.Dto;
using Kiosk.WebAPI.Persistance;
using System.Xml.Linq;

namespace Kiosk.WebAPI.Db.Dto.Validators
{
    public class RegisterCreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        private readonly IProductService _productService;   
        public RegisterCreateProductDtoValidator(IProductService productService)
        {
            _productService = productService;
            // ToDo: reguły walidacyjne 
            RuleFor(p => p.Name)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(20)
            .Must(BeUniqueName)
            .WithMessage("This category name already exists.");




            RuleFor(p => p.UnitPrice)
                .NotEmpty()
                .GreaterThan(0.01m) //m  jawna deklaracja ze wartosc to decimal
                .WithMessage("Unit price has to be greater then 1 grosz");

        }

        private bool BeUniqueName(string name)
        {
            return _productService.IsInUse(name);
        }

      

    }
}
