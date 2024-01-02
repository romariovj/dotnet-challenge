using AutoMapper;
using DotnetChallenge.Application.CreateProduct.Commands;
using DotnetChallenge.Application.Dtos;
using DotnetChallenge.Domain.Entities;
using DotnetChallenge.Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DotnetChallenge.Application.CreateProduct.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ActionResult<ProductDto>>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ActionResult<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await new CreateProductCommandValidator().ValidateAsync(request, cancellationToken);

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
            var result = await _repository.CreateAsync(domainProduct);
            return _mapper.Map<ProductDto>(result);
        }
    }
}
