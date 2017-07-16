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

namespace Ixq.Demo.Web.Areas.Hplus.Controllers
{
    public class ProductTypeController : EntityController<ProductType, ProductTypeDot, Guid>
    {
        public ProductTypeController(IRepository<ProductType> repository) : base(repository)
        {
        }

        public override IQueryable<ProductType> EntityQueryable(Expression<Func<ProductType, bool>> predicate = null)
        {
            if (predicate != null) return Repository.GetDbSet().Include("ParentType").Where(predicate);
            return Repository.GetDbSet().Include("ParentType");
        }
    }
}