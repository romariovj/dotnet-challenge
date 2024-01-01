using AutoMapper;
using DotnetChallenge.Domain.Entities;
using DotnetChallenge.Infrastructure.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetChallenge.Infrastructure.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Product, ProductEntity>();
            CreateMap<ProductEntity, Product>();
        }
    }
}
