using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetChallenge.Application.UpdateProduct.Commands
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator() 
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(request => request.Id)
                .NotNull()
                .NotEmpty().WithMessage("El id del producto es obligatorio.");

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
