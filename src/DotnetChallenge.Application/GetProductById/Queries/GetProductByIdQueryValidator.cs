using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetChallenge.Application.GetProductById.Queries
{
    public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
    {
        public GetProductByIdQueryValidator() 
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(request => request.Id)
                .NotNull()
                .NotEmpty().WithMessage("El Id es obligatorio");
        }
    }
}
