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
    public abstract class ApplicationUserManagerBase<TUser> : UserManager<TUser>, IUserManager<IUser>, IScopeDependency
        where TUser : class, IUser
    {
        private readonly UserManager<TUser> _userManager;

        protected ApplicationUserManagerBase(IUserStore<TUser> store) : base(store)
        {
            _userManager = this;
        }

        IQueryable<IUser> IUserManager<IUser>.Users => _userManager.Users;

        public bool AddToRole(string userId, string roleId)
        {
            throw new NotImplementedException();
        }

        public bool AddToRoles(string userId, IList<string> roles)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddToRolesAsync(string userId, IList<string> roles)
        {
            throw new NotImplementedException();
        }

        public bool CheckPassword(IUser user, string password)
        {
            _userManager.CheckPassword((TUser)user, password);
            throw new NotImplementedException();
        }

        public bool CheckPassword(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckPasswordAsync(IUser user, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckPasswordAsync(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public ReturnModel Create(IUser user)
        {
            throw new NotImplementedException();
        }

        public Task<ReturnModel> CreateAsync(IUser user)
        {
            throw new NotImplementedException();
        }

        public ReturnModel Delete(string userId)
        {
            throw new NotImplementedException();
        }

        public ReturnModel Delete(IUser user)
        {
            throw new NotImplementedException();
        }

        public Task<ReturnModel> DeleteAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<ReturnModel> DeleteAsync(IUser user)
        {
            throw new NotImplementedException();
        }

        public IUser Find(string userId)
        {
            throw new NotImplementedException();
        }

        public IUser Find(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public Task<IUser> FindAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public IUser FindByName(string userName)
        {
            return _userManager.FindByName(userName);
        }

        public string GetUserName(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserNameAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public List<string> GetUserRoles(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> GetUserRolesAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public List<string> GetUserRolesByName(string userName)
        {
            var user = FindByName(userName);
            return _userManager.GetRoles(user.Id).ToList();
        }

        public Task<List<string>> GetUserRolesByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public bool HasUser(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasUserAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public bool IsInRole(string userId, string role)
        {
            throw new NotImplementedException();
        }

        public ReturnModel Update(IUser user)
        {
            throw new NotImplementedException();
        }

        public Task<ReturnModel> UpdateAsync(IUser user)
        {
            throw new NotImplementedException();
        }

        Task<bool> IUserManager<IUser>.AddToRoleAsync(string userId, string roleId)
        {
            throw new NotImplementedException();
        }

        Task<IUser> IUserManager<IUser>.FindAsync(string userName, string password)
        {
            throw new NotImplementedException();
        }

        Task<IUser> IUserManager<IUser>.FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }
    }
}