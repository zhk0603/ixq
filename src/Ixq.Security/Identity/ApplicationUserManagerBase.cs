using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ixq.Core.DependencyInjection;
using Ixq.Core.Entity;
using Ixq.Core.Security;
using Ixq.Extensions.ObjectModel;
using Microsoft.AspNet.Identity;

namespace Ixq.Security.Identity
{
    public abstract class ApplicationUserManagerBase<TUser> : UserManager<TUser>, IUserManager<IUser<string>>, IScopeDependency
        where TUser : class, IUser<string>
    {
        private readonly UserManager<TUser> _userManager;

        IQueryable<IUser<string>> IUserManager<IUser<string>>.Users
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        protected ApplicationUserManagerBase(IUserStore<TUser> store) : base(store)
        {
            _userManager = this;
        }

        public ReturnModel Create(TUser user)
        {
            var vm = _userManager.Create(user);
            var resultVm = new ReturnModel(vm.Errors) {Succeeded = vm.Succeeded};
            return resultVm;
        }

        public new async Task<ReturnModel> CreateAsync(TUser user)
        {
            var vm = await _userManager.CreateAsync(user);
            var resultVm = new ReturnModel(vm.Errors) {Succeeded = vm.Succeeded};
            return resultVm;
        }

        public TUser Find(string userId)
        {
            return _userManager.FindById(userId);
        }

        public Task<TUser> FindAsync(string userId)
        {
            return _userManager.FindByIdAsync(userId);
        }

        public TUser Find(string userName, string password)
        {
            return _userManager.Find(userName, password);
        }

        public TUser FindByName(string userName)
        {
            return _userManager.FindByName(userName);
        }

        public bool HasUser(string userName)
        {
            return FindByName(userName) != null;
        }

        public Task<bool> HasUserAsync(string userName)
        {
            return Task.FromResult(HasUser(userName));
        }

        public ReturnModel Delete(TUser user)
        {
            var vm = _userManager.Delete(user);
            var resultVm = new ReturnModel(vm.Errors) {Succeeded = vm.Succeeded};
            return resultVm;
        }

        public new async Task<ReturnModel> DeleteAsync(TUser user)
        {
            var vm = await _userManager.DeleteAsync(user);
            var resultVm = new ReturnModel(vm.Errors) {Succeeded = vm.Succeeded};
            return resultVm;
        }

        public ReturnModel Delete(string userId)
        {
            var user = Find(userId);
            var vm = _userManager.Delete(user);
            var resultVm = new ReturnModel(vm.Errors) {Succeeded = vm.Succeeded};
            return resultVm;
        }

        public async Task<ReturnModel> DeleteAsync(string userId)
        {
            var user = await FindAsync(userId);
            var vm = await _userManager.DeleteAsync(user);
            var resultVm = new ReturnModel(vm.Errors) {Succeeded = vm.Succeeded};
            return resultVm;
        }

        public ReturnModel Update(TUser user)
        {
            var vm = _userManager.Update(user);
            var resultVm = new ReturnModel(vm.Errors) {Succeeded = vm.Succeeded};
            return resultVm;
        }

        public new async Task<ReturnModel> UpdateAsync(TUser user)
        {
            var vm = await _userManager.UpdateAsync(user);
            var resultVm = new ReturnModel(vm.Errors) {Succeeded = vm.Succeeded};
            return resultVm;
        }

        public bool CheckPassword(string userName, string password)
        {
            var user = FindByName(userName);
            return CheckPassword(user, password);
        }

        public async Task<bool> CheckPasswordAsync(string userName, string password)
        {
            var user = await FindByNameAsync(userName);
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public bool CheckPassword(TUser user, string password)
        {
            return _userManager.CheckPassword(user, password);
        }

        public string GetUserName(string userId)
        {
            return Find(userId).UserName;
        }

        public Task<string> GetUserNameAsync(string userId)
        {
            return Task.FromResult(GetUserName(userId));
        }

        public bool AddToRole(string userId, string roleId)
        {
            return _userManager.AddToRole(userId, roleId).Succeeded;
        }

        public new Task<bool> AddToRoleAsync(string userId, string roleId)
        {
            return Task.FromResult(AddToRole(userId, roleId));
        }

        public bool AddToRoles(string userId, IList<string> roles)
        {
            return _userManager.AddToRoles(userId, roles.ToArray()).Succeeded;
        }

        public Task<bool> AddToRolesAsync(string userId, IList<string> roles)
        {
            return Task.FromResult(AddToRoles(userId, roles));
        }

        public List<string> GetUserRoles(string userId)
        {
            return _userManager.GetRoles(userId).ToList();
        }

        public Task<List<string>> GetUserRolesAsync(string userId)
        {
            return Task.FromResult(GetUserRoles(userId));
        }

        public List<string> GetUserRolesByName(string userName)
        {
            var user = FindByName(userName);
            return GetUserRoles(user.Id);
        }

        public Task<List<string>> GetUserRolesByNameAsync(string userName)
        {
            return Task.FromResult(GetUserRolesByName(userName));
        }

        public bool IsInRole(string userId, string role)
        {
            return _userManager.IsInRole(userId, role);
        }

        public ReturnModel Create(IUser<string> user)
        {
            throw new NotImplementedException();
        }

        public Task<ReturnModel> CreateAsync(IUser<string> user)
        {
            throw new NotImplementedException();
        }

        IUser<string> IUserManager<IUser<string>>.Find(string userId)
        {
            throw new NotImplementedException();
        }

        Task<IUser<string>> IUserManager<IUser<string>>.FindAsync(string userId)
        {
            throw new NotImplementedException();
        }

        IUser<string> IUserManager<IUser<string>>.Find(string userName, string password)
        {
            throw new NotImplementedException();
        }

        Task<IUser<string>> IUserManager<IUser<string>>.FindAsync(string userName, string password)
        {
            throw new NotImplementedException();
        }

        IUser<string> IUserManager<IUser<string>>.FindByName(string userName)
        {
            throw new NotImplementedException();
        }

        Task<IUser<string>> IUserManager<IUser<string>>.FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public ReturnModel Delete(IUser<string> user)
        {
            throw new NotImplementedException();
        }

        public Task<ReturnModel> DeleteAsync(IUser<string> user)
        {
            throw new NotImplementedException();
        }

        public ReturnModel Update(IUser<string> user)
        {
            throw new NotImplementedException();
        }

        public Task<ReturnModel> UpdateAsync(IUser<string> user)
        {
            throw new NotImplementedException();
        }

        public bool CheckPassword(IUser<string> user, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckPasswordAsync(IUser<string> user, string password)
        {
            throw new NotImplementedException();
        }
    }
}