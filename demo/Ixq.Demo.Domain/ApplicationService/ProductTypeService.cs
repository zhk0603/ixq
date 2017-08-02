using System;
using System.Web.Routing;
using Ixq.Core.Repository;
using Ixq.Demo.Domain.IApplicationService;
using Ixq.Demo.Entities;
using Ixq.Demo.Domain.Dtos;
using Ixq.Web.Mvc;
using System.Linq;
using Ixq.Data.Repository.Extensions;

namespace Ixq.Demo.Domain.ApplicationService
{
    public class ProductTypeService : EntityService<ProductType, ProductTypeDot, Guid>, IProductTypeService
    {
        public ProductTypeService(IRepositoryBase<ProductType, Guid> repository, RequestContext requestContxt,
            IEntityControllerData entityControllerData) :
                base(repository, requestContxt, entityControllerData)
        {
        }

        public override IQueryable<ProductType> Query()
        {
            return Repository.GetAllInclude(x => x.ParentType);
        }
    }
}