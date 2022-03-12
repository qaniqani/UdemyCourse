using System.Collections.Generic;
using Abp.Events.Bus;
using Udemy.Course.Products;

namespace Udemy.Course.EventDatas
{
    public class AddProductEventData : EventData
    {
        public List<ProductMapping> Products { get; set; }
    }
}