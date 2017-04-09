using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ixq.Core.Repository;
using Ixq.Demo.Entities;
using Ixq.Core.DependencyInjection.Extensions;
using Ixq.Data.Repository.Extensions;

namespace Ixq.Demo.Web.Controllers
{
    public class ProductTypeController : BaseController
    {
        private readonly IRepository<ProductType> _productTypeRepository;
        public IRepository<ProductType> ProductTypeRepository { get; set; }
        public IRepository<Product> ProductRepository { get; set; }
        public ProductTypeController(IRepository<ProductType> productTypeRepository)
        {
            _productTypeRepository = productTypeRepository;
        }

        // GET: ProductType
        public ActionResult Index()
        {
            var a = _productTypeRepository.GetHashCode();
            var b = ProductTypeRepository.GetHashCode();

            var c = _productTypeRepository.UnitOfWork.GetHashCode();
            var d = ProductTypeRepository.UnitOfWork.GetHashCode();

            var a1 = ProductRepository.GetHashCode();
            var b1 = ProductRepository.UnitOfWork.GetHashCode();

            var c1 = ServiceProvider.GetService<IRepository<ProductType>>().GetHashCode();
            var c2 = ServiceProvider.GetService<IRepository<Product>>().GetHashCode();

            var c3 = ServiceProvider.GetRepository<Product>().GetHashCode();

            return View();
        }
    }
}