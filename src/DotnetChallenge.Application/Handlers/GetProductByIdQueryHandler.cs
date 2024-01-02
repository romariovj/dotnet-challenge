using AutoMapper;
using DotnetChallenge.Application.Dtos;
using DotnetChallenge.Application.Queries;
using DotnetChallenge.Domain.Entities;
using DotnetChallenge.Domain.Repositories;
using MediatR;

namespace DotnetChallenge.Application.Handlers
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            Product? product = await _repository.FindByIdAsync(request.Id);
            return _mapper.Map<ProductDto>(product);
        }
    }
}
