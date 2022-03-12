using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.MultiTenancy;
using Abp.Web.Models;
using Microsoft.EntityFrameworkCore;
using Udemy.Course.Authorization;
using Udemy.Course.Products.Dto;
using Udemy.Course.Products.Inputs;

namespace Udemy.Course.Products
{
    public interface IProductAppService : IApplicationService
    {
        Task<ProductDto> CreateOrUpdate(ProductDto input);
        Task<List<ProductDto>> GetProducts(GetProductInput input);
        Task<List<ProductDto>> GetCustomerProduct(CustomerProductInput input);
        Task<PagedResultDto<ProductDto>> GetAll(ProductGetAllInput input);
    }
    
    [AbpAuthorize(PermissionNames.Pages_Products)]
    public class ProductAppService : ApplicationService, IProductAppService
    {
        private readonly IRepository<Product> _product;
        private readonly IRepository<ProductMapping> _productMapping;
        private readonly IProductDomainService _productDomainService;

        public ProductAppService(IRepository<Product> product, IRepository<ProductMapping> productMapping, IProductDomainService productDomainService)
        {
            _product = product;
            _productMapping = productMapping;
            _productDomainService = productDomainService;
        }
        
        [AbpAuthorize(PermissionNames.Pages_Products_CustomerProduct)]
        public async Task<List<ProductDto>> GetCustomerProduct(CustomerProductInput input)
        {
            var customerProducts =
                await _productMapping.GetAll()
                    .Where(a => a.CustomerId.HasValue && a.CustomerId == input.CustomerId)
                    .ToListAsync();

            var productIds = customerProducts.Select(a => a.CustomerId.Value).ToArray();

            var products = await _product.GetAll()
                .Where(a => productIds.Contains(a.Id))
                .ToListAsync();

            var result = ObjectMapper.Map<List<ProductDto>>(products);

            return result;
        }

        public async Task<PagedResultDto<ProductDto>> GetAll(ProductGetAllInput input)
        {
            var q = _product.GetAll()
                .WhereIf(input.MinPrice.HasValue, a => a.Price >= input.MinPrice.Value)
                .WhereIf(input.MaxPrice.HasValue, a => a.Price <= input.MaxPrice.Value)
                .WhereIf(!string.IsNullOrEmpty(input.Name), a => a.Name.Contains(input.Name))
                .WhereIf(input.ProductId.HasValue, a => a.Id == input.ProductId.Value);

            q = q.OrderBy(!string.IsNullOrEmpty(input.Sorting) ? input.Sorting : "name asc");

            var totalCount = await q.CountAsync();

            var products = await q.Skip(input.SkipCount).Take(input.MaxResultCount).ToListAsync();

            var productDto = ObjectMapper.Map<List<ProductDto>>(products);

            var result = new PagedResultDto<ProductDto>(totalCount, productDto);

            return result;
        }
        
        public async Task<List<ProductDto>> GetProducts(GetProductInput input)
        {
            var products = await _productDomainService.GetCustomerProducts(input.CustomerId, AbpSession.MultiTenancySide, input.TenantId);
            var result = ObjectMapper.Map<List<ProductDto>>(products);
            return result;
        }
        
        [AbpAuthorize(PermissionNames.Pages_Products_CreateOrUpdate)]
        public async Task<ProductDto> CreateOrUpdate(ProductDto input)
        {
            var p = ObjectMapper.Map<Product>(input);

            var product = await _product.InsertOrUpdateAsync(p);

            var dto = ObjectMapper.Map<ProductDto>(product);

            return dto;
        }
    }
}


















