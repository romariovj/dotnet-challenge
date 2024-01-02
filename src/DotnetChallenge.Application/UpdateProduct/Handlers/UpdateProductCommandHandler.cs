using AutoMapper;
using DotnetChallenge.Application.Dtos;
using DotnetChallenge.Application.UpdateProduct.Commands;
using DotnetChallenge.Domain.Entities;
using DotnetChallenge.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DotnetChallenge.Application.UpdateProduct.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ActionResult<ProductDto>>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ActionResult<ProductDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await new UpdateProductCommandValidator().ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(error => new
                {
                    PropertyName = error.PropertyName,
                    ErrorMessage = error.ErrorMessage
                });

                return new BadRequestObjectResult(new
                {
                    Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                    Title = "One or more validation errors occurred.",
                    Status = 400,
                    Errors = errors,
                    TraceId = Activity.Current?.Id
                });
            }

            var domainProduct = _mapper.Map<Product>(request);
            Product? product = await _repository.UpdateAsync(domainProduct);
            return _mapper.Map<ProductDto>(product);
        }
    }
}
