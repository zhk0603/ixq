﻿using System;
using System.Web.Mvc;

namespace Ixq.Demo.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        public IServiceProvider ServiceProvider { get; set; }
    }
}