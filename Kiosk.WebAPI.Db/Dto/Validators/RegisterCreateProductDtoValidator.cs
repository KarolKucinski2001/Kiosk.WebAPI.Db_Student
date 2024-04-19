using FluentValidation;
using Kiosk.WebAPI.Dto;

namespace Kiosk.WebAPI.Db.Dto.Validators
{
    public class RegisterCreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        public RegisterCreateProductDtoValidator()
        {
            // ToDo: reguły walidacyjne 
            RuleFor(p => p.Name)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(20);
        }
    }
}
