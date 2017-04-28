using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ixq.Core.Repository;
using Ixq.Demo.Domain.Dtos;
using Ixq.Demo.Entities;
using Ixq.Web.Mvc;

namespace Ixq.Demo.Web.Areas.Hplus.Controllers
{
    public class ProductTypeController : EntityController<ProductType, ProductTypeDot>
    {
        public ProductTypeController(IRepository<ProductType> repository) : base(repository)
        {
        }

        public new ActionResult Index()
        {
            return View();
        }
    }
}