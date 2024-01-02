using AutoMapper;
using DotnetChallenge.Application.Commands;
using DotnetChallenge.Application.Dtos;
using DotnetChallenge.Domain.Entities;
using DotnetChallenge.Domain.Repositories;
using MediatR;

namespace DotnetChallenge.Application.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var domainProduct = _mapper.Map<Product>(request);
            Product? product = await _repository.UpdateAsync(domainProduct);
            return _mapper.Map<ProductDto>(product);
        }
    }
}
