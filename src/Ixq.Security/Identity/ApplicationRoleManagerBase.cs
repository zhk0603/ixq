using System;
using System.Linq;
using System.Threading.Tasks;
using Ixq.Core.DependencyInjection;
using Ixq.Core.Entity;
using Ixq.Core.Security;
using Ixq.Extensions.ObjectModel;
using Microsoft.AspNet.Identity;

namespace Ixq.Security.Identity
{
    public abstract class ApplicationRoleManagerBase<TRole> : RoleManager<TRole>, IRoleManager<IRole>, IScopeDependency
        where TRole : class, IRole
    {
        private readonly RoleManager<TRole> _roleManager;
        protected ApplicationRoleManagerBase(IRoleStore<TRole, string> store) : base(store)
        {
            _roleManager = this;
        }

        IQueryable<IRole> IRoleManager<IRole>.Roles
        {
            get
            {
                return _roleManager.Roles;
            }
        }

        public ReturnModel Create(IRole role)
        {
            throw new NotImplementedException();
        }

        public Task<ReturnModel> CreateAsync(IRole role)
        {
            throw new NotImplementedException();
        }

        public ReturnModel Delete(string roleId)
        {
            throw new NotImplementedException();
        }

        public ReturnModel Delete(IRole role)
        {
            throw new NotImplementedException();
        }

        public Task<ReturnModel> DeleteAsync(string roleId)
        {
            throw new NotImplementedException();
        }

        public Task<ReturnModel> DeleteAsync(IRole role)
        {
            throw new NotImplementedException();
        }

        public IRole Find(string roleId)
        {
            throw new NotImplementedException();
        }

        public Task<IRole> FindAsync(string roleId)
        {
            throw new NotImplementedException();
        }

        public IRole FindByName(string roleName)
        {
            throw new NotImplementedException();
        }

        public string GetRoleName(string roleId)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRoleNameAsync(string roleId)
        {
            throw new NotImplementedException();
        }

        public bool HasRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasRoleAsync(string roleName)
        {
            throw new NotImplementedException();
        }

        public ReturnModel Update(IRole role)
        {
            throw new NotImplementedException();
        }

        public Task<ReturnModel> UpdateAsync(IRole role)
        {
            throw new NotImplementedException();
        }

        Task<IRole> IRoleManager<IRole>.FindByNameAsync(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}