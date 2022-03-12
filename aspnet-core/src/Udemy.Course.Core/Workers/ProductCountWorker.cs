using System;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Logging;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using Castle.Core.Logging;
using Udemy.Course.Products;

namespace Udemy.Course.Workers
{
    public class ProductCountWorker : PeriodicBackgroundWorkerBase, ISingletonDependency
    {
        private readonly ILogger _logger;
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Product> _product;
        
        public ProductCountWorker(AbpTimer timer, IRepository<Product> product, IUnitOfWork unitOfWork, ILogger logger) : base(timer)
        {
            _product = product;
            _unitOfWork = unitOfWork;
            _logger = logger;

            Timer.Period = (int) TimeSpan.FromSeconds(5).TotalMilliseconds;
        }

        [UnitOfWork]
        protected override void DoWork()
        {
            using (_unitOfWork.DisableFilter(AbpDataFilters.MayHaveTenant, AbpDataFilters.MustHaveTenant))
            {
                // bir takim senaryolar...
                
                _logger.Info("WORKER CALISTI!!!");
            }
        }
    }
}