using Ixq.Demo.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ixq.Demo.DbContext
{
    public class BaseDataContext : Ixq.Data.Repository.DbContextBase
    {
        public BaseDataContext()
        {
            Initialize();
            Configuration.LazyLoadingEnabled = false;
            Configuration.ValidateOnSaveEnabled = false;
        }

        public IDbSet<Test> Tests { get; set; }
    }
}
