using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Events.Bus;
using Abp.Linq.Extensions;
using Abp.MultiTenancy;
using Abp.Web.Models;
using Microsoft.EntityFrameworkCore;
using Udemy.Course.Authorization;
using Udemy.Course.Customers.Dto;
using Udemy.Course.Customers.Inputs;
using Udemy.Course.EventDatas;
using Udemy.Course.Products;
using Udemy.Course.Products.Dto;

namespace Udemy.Course.Customers
{
    public interface ICustomerAppService : IAsyncCrudAppService<CustomerDto, int, CustomerGetAllInput,CustomerDto, CustomerUpdateDto>
    {
        Task<CustomerDto> CreateOrUpdate(CustomerDto input);
        Task<List<ProductDto>> GetProducts(GetProductInput input);
        Task<CustomerDto> CreateCustomerAndProduct(CreateCustomerAndProductInput input);
    }
    
    [AbpAuthorize(PermissionNames.Pages_Customers)]
    public class CustomerAppService : AsyncCrudAppService<Customer, CustomerDto, int, CustomerGetAllInput,CustomerDto, CustomerUpdateDto>, ICustomerAppService
    {
        private readonly IProductDomainService _productDomainService;
        
        public CustomerAppService(IRepository<Customer, int> repository, IProductDomainService productDomainService) : base(repository)
        {
            _productDomainService = productDomainService;
            GetPermissionName = PermissionNames.Pages_Customers;
            CreatePermissionName = PermissionNames.Pages_Customers_Create;
            UpdatePermissionName = PermissionNames.Pages_Customers_Update;
            DeletePermissionName = PermissionNames.Pages_Customers_Delete;
            GetAllPermissionName = PermissionNames.Pages_Customers_GetAll;
        }

        protected override IQueryable<Customer> CreateFilteredQuery(CustomerGetAllInput input)
        {
            return base.CreateFilteredQuery(input)
                .WhereIf(!string.IsNullOrEmpty(input.Name), a => a.Name.Contains(input.Name))
                .WhereIf(!string.IsNullOrEmpty(input.Surname), a => a.Surname.Contains(input.Surname));
        }

        public async Task GetDeletedCustomers()
        {
            var q = Repository.GetAll().Where(a => a.IsDeleted);
            if (AbpSession.MultiTenancySide == MultiTenancySides.Host)
            {
                using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.MayHaveTenant, AbpDataFilters.MustHaveTenant, AbpDataFilters.SoftDelete))
                {
                    var customers = await q.ToListAsync();
                }
            }
            else
            {
                using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete))
                {
                    var customers = await q.ToListAsync();
                }
            }
        }

        public async Task<List<ProductDto>> GetProducts(GetProductInput input)
        {
            var products = await _productDomainService.GetCustomerProducts(input.CustomerId, AbpSession.MultiTenancySide, input.TenantId);

            var result = ObjectMapper.Map<List<ProductDto>>(products);

            return result;
        }

        public async Task<CustomerDto> CreateCustomerAndProduct(CreateCustomerAndProductInput input)
        {
            var customer = await Repository.InsertAsync(new Customer
            {
                Name = input.Name,
                Surname = input.Surname
            });

            await CurrentUnitOfWork.SaveChangesAsync();

            var productMappings = ObjectMapper.Map<List<ProductMapping>>(input.Products);
            
            productMappings.ForEach(a =>
            {
                a.CustomerId = customer.Id;
                a.TenantId = AbpSession.TenantId;
            });

            await EventBus.Default.TriggerAsync(new AddProductEventData
            {
                Products = productMappings
            });

            return ObjectMapper.Map<CustomerDto>(customer);
        }

        [UnitOfWork]
        public async Task<CustomerDto> CreateOrUpdate(CustomerDto input)
        {
            var c = ObjectMapper.Map<Customer>(input);

            c.TenantId = AbpSession.TenantId.Value;

            var customer = await Repository.InsertOrUpdateAsync(c);

            return ObjectMapper.Map<CustomerDto>(customer);
        }
    }
}











