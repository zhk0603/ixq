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
    public abstract class ApplicationRoleManagerBase<TRole> : RoleManager<TRole>, IRoleManager<TRole>, IScopeDependency
        where TRole : class, IRole<string>
    {
        private readonly RoleManager<TRole> _roleManager;
        protected ApplicationRoleManagerBase(IRoleStore<TRole, string> store) : base(store)
        {
            _roleManager = this;
        }

        public ReturnModel Create(TRole role)
        {
            var vm = _roleManager.Create(role);
            var resultVm = new ReturnModel(vm.Errors) { Succeeded = vm.Succeeded };
            return resultVm;
        }

        public new async Task<ReturnModel> CreateAsync(TRole role)
        {
            var vm = await _roleManager.CreateAsync(role);
            var resultVm = new ReturnModel(vm.Errors) { Succeeded = vm.Succeeded };
            return resultVm;
        }

        public TRole Find(string roleId)
        {
            return _roleManager.FindById(roleId);
        }

        public Task<TRole> FindAsync(string roleId)
        {
            return _roleManager.FindByIdAsync(roleId);
        }

        public TRole FindByName(string roleName)
        {
            return _roleManager.FindByName(roleName);
        }

        public bool HasRole(string roleName)
        {
            return _roleManager.RoleExists(roleName);
        }

        public Task<bool> HasRoleAsync(string roleName)
        {
            return _roleManager.RoleExistsAsync(roleName);
        }

        public ReturnModel Delete(TRole role)
        {
            var vm = _roleManager.Delete(role);
            var resultVm = new ReturnModel(vm.Errors) { Succeeded = vm.Succeeded };
            return resultVm;
        }

        public new async Task<ReturnModel> DeleteAsync(TRole role)
        {
            var vm = await _roleManager.DeleteAsync(role);
            var resultVm = new ReturnModel(vm.Errors) { Succeeded = vm.Succeeded };
            return resultVm;
        }

        public ReturnModel Delete(string roleId)
        {
            var role = _roleManager.FindById(roleId);
            return Delete(role);
        }

        public Task<ReturnModel> DeleteAsync(string roleId)
        {
            var role = _roleManager.FindById(roleId);
            return DeleteAsync(role);
        }

        public ReturnModel Update(TRole role)
        {
            var vm = _roleManager.Update(role);
            var resultVm = new ReturnModel(vm.Errors) { Succeeded = vm.Succeeded };
            return resultVm;
        }

        public new async Task<ReturnModel> UpdateAsync(TRole role)
        {
            var vm = await _roleManager.UpdateAsync(role);
            var resultVm = new ReturnModel(vm.Errors) { Succeeded = vm.Succeeded };
            return resultVm;
        }

        public string GetRoleName(string roleId)
        {
            return _roleManager.FindById(roleId).Name;
        }

        public Task<string> GetRoleNameAsync(string roleId)
        {
            return Task.FromResult(GetRoleName(roleId));
        }
    }
}