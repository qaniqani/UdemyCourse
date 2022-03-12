using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Events.Bus.Handlers;
using Udemy.Course.EventDatas;
using Udemy.Course.Products;

namespace Udemy.Course.EventHandlers
{
    public class AddProductEventHandler : IEventHandler<AddProductEventData>, ITransientDependency
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<ProductMapping> _productMapping;

        public AddProductEventHandler(IRepository<ProductMapping> productMapping, IUnitOfWork unitOfWork)
        {
            _productMapping = productMapping;
            _unitOfWork = unitOfWork;
        }

        [UnitOfWork]
        public async void HandleEvent(AddProductEventData eventData)
        {
            foreach (var p in eventData.Products)
            {
                await _productMapping.InsertAsync(p);
            }
            
            await _unitOfWork.SaveChangesAsync();
        }
    }
}