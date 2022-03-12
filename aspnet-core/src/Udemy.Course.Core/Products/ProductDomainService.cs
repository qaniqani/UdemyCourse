using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.MultiTenancy;
using Microsoft.EntityFrameworkCore;

namespace Udemy.Course.Products
{
    public interface IProductDomainService : IDomainService
    {
        Task<List<Product>> GetCustomerProducts(int customerId, MultiTenancySides sides, int? tenantId);
    }
    
    public class ProductDomainService : DomainService, IProductDomainService
    {
        private readonly IRepository<Product> _product;
        private readonly IRepository<ProductMapping> _productMapping;

        private readonly IUnitOfWork _unitOfWork;

        public ProductDomainService(IRepository<Product> product, IRepository<ProductMapping> productMapping, IUnitOfWork unitOfWork)
        {
            _product = product;
            _productMapping = productMapping;
            _unitOfWork = unitOfWork;
        }

        [UnitOfWork]
        public async Task<List<Product>> GetCustomerProducts(int customerId, MultiTenancySides sides, int? tenantId)
        {
            var q = _productMapping.GetAll()
                .Where(a => a.CustomerId.HasValue && a.CustomerId == customerId);

            int[] productIds;
            if (sides == MultiTenancySides.Host)
            {
                using (_unitOfWork.DisableFilter(AbpDataFilters.MayHaveTenant))
                {
                    if (tenantId.HasValue)
                    {
                        using (_unitOfWork.SetTenantId(tenantId.Value))
                        {
                            productIds = await q.Select(a => a.ProductId).ToArrayAsync();
                        }
                    }
                    else
                        productIds = await q.Select(a => a.ProductId).ToArrayAsync();
                }
            }
            else
                productIds = await q.Select(a => a.ProductId).ToArrayAsync();

            var products = await _product.GetAll()
                .Where(a => productIds.Contains(a.Id))
                .AsNoTracking()
                .ToListAsync();

            return products;
        }
    }
}