using System;
using System.Linq;
using System.Web.Mvc;
using Ixq.Core.DependencyInjection.Extensions;
using Ixq.Core.Mapper;
using Ixq.Core.Repository;
using Ixq.Data.Repository.Extensions;
using Ixq.Demo.Entities;
using System.Data.Entity;
using ixq.Demo.DbContext;
using Ixq.Core.Entity;
using Ixq.Demo.Domain.Dtos;
using Ixq.Demo.Domain.IApplicationService;

namespace Ixq.Demo.Web.Controllers
{
    public class ProductTypeController : BaseController
    {
        private readonly IRepository<ProductType> _productTypeRepository;

        public ProductTypeController(IRepository<ProductType> productTypeRepository)
        {
            _productTypeRepository = productTypeRepository;
        }

        public IRepository<ProductType> ProductTypeRepository { get; set; }
        public IRepository<Product> ProductRepository { get; set; }

        // GET: ProductType
        public ActionResult Index()
        {
            Logger.Debug("Debug", new Exception("asdfasdfadsf"));
            Logger.Error("Error");
            Logger.Fatal("Fatal");
            Logger.Info("Info");
            Logger.Warn("Warn");

            var a = _productTypeRepository.GetHashCode();
            var b = ProductTypeRepository.GetHashCode();

            var c = _productTypeRepository.UnitOfWork.GetHashCode();
            var d = ProductTypeRepository.UnitOfWork.GetHashCode();

            var a1 = ProductRepository.GetHashCode();
            var b1 = ProductRepository.UnitOfWork.GetHashCode();

            var c1 = ServiceProvider.GetService<IRepository<ProductType>>().GetHashCode();
            var c2 = ServiceProvider.GetService<IRepository<Product>>().GetHashCode();

            var aa2 = ServiceProvider.GetService<IRepositoryBase<ProductType, Guid>>().GetHashCode();

            return View();
        }

        public ActionResult Index1()
        {
            var mapper = ServiceProvider.GetService<IMapper>();
            var dto1 = new ProductTypeDot
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                ParentType = null
            };
            var ent = dto1.MapTo();
            var dto = ent.MapToDto<ProductTypeDot>();

            return View();
        }

        // test url : locahost/ProductType/ProductDto?Id=8A2734FF-4C33-48ED-8257-9E3B612FF38F&Type.Id=8A2734FF-4C33-48ED-8257-9E3B612FF38A&Name=123
        public ActionResult ProductDto(ProductDto input)
        {
            var p = input.MapTo();
            return View();
        }


        public ActionResult TestAction1()
        {

            var allType =
                _productTypeRepository.GetAll()
                    .Include(x => x.ParentType)
                    .ToDtoList<ProductTypeDot, ProductType, Guid>();

            var context = new DataContext();
            var allProductType = context.ProductTypes.Include(x => x.ParentType).ToList();
            var allProduct = context.Products.Include("Type").ToList();

            return View();
        }
    }
}