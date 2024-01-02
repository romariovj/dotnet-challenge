using AutoMapper;
using DotnetChallenge.Application.Commands;
using DotnetChallenge.Application.Dtos;
using DotnetChallenge.Domain.Entities;
using DotnetChallenge.Domain.Repositories;
using MediatR;

namespace DotnetChallenge.Application.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var domainProduct = _mapper.Map<Product>(request);
            var result = await _repository.CreateAsync(domainProduct);
            return _mapper.Map<ProductDto>(result);
        }
    }
}
