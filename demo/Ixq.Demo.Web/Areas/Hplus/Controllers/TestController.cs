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
    public class TestController : EntityController<Test,TestDto,int>
    {
        public TestController(IRepositoryInt32<Test> repository) : base(repository)
        {
        }
    }
}