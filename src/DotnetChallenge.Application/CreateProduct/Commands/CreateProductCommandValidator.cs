using FluentValidation;

namespace DotnetChallenge.Application.CreateProduct.Commands
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator() 
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(request => request.Name)
                .NotNull()
                .NotEmpty().WithMessage("El nombre del producto es obligatorio.");

            RuleFor(request => request.Status)
                .NotNull()
                .NotEmpty()
                .InclusiveBetween(0, 1).WithMessage("El estado del producto debe ser 0 o 1.");


            RuleFor(request => request.Stock)
                .GreaterThanOrEqualTo(1).WithMessage("El stock del producto debe ser mayor o igual a 1.");

            RuleFor(request => request.Description)
                .NotNull()
                .NotEmpty()
                .MaximumLength(255).WithMessage("La descripción del producto no debe superar los 255 caracteres.");

            RuleFor(request => request.Price)
                .GreaterThan(0).WithMessage("El precio del producto debe ser mayor que 0.");

        }
    }
}
