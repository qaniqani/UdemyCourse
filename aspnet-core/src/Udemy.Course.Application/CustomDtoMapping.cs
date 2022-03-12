using AutoMapper;
using Udemy.Course.Products;
using Udemy.Course.Products.Dto;

namespace Udemy.Course
{
    internal static class CustomDtoMapping
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}