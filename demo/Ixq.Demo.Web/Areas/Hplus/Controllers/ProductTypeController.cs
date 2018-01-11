using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Ixq.Core.Repository;
using Ixq.Demo.Domain.Dtos;
using Ixq.Demo.Entities;
using Ixq.Web.Mvc;
using Ixq.Data.Repository.Extensions;
using System.Web.Routing;
using Ixq.Demo.Domain.ApplicationService;

namespace Ixq.Demo.Web.Areas.Hplus.Controllers
{
    public class ProductTypeController : EntityController<ProductType, ProductTypeDot, Guid>
    {
        public ProductTypeController(IRepository<ProductType> repository) : base(repository)
        {
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            this.EntityService = new ProductTypeService(Repository, this.Request.RequestContext, this);
        }
    }
}