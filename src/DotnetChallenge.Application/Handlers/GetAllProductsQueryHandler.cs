using AutoMapper;
using DotnetChallenge.Application.Dtos;
using DotnetChallenge.Application.Queries;
using DotnetChallenge.Domain.Entities;
using DotnetChallenge.Domain.Repositories;
using MediatR;

namespace DotnetChallenge.Application.Handlers
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            IReadOnlyList<Product> products = await _repository.FindAllAsync();
            return _mapper.Map<List<ProductDto>>(products);

        }
    }
}
