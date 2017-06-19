using Ixq.Core.Entity;
using Ixq.Extensions.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.DependencyInjection;

namespace Ixq.Core.Security
{
    public interface IUserManager<TUser> : IScopeDependency
        where TUser : class
    {
        IQueryable<TUser> Users { get; }
        ReturnModel Create(TUser user);
        Task<ReturnModel> CreateAsync(TUser user);
        TUser Find(string userId);
        Task<TUser> FindAsync(string userId);
        TUser Find(string userName, string password);
        Task<TUser> FindAsync(string userName, string password);
        TUser FindByName(string userName);
        Task<TUser> FindByNameAsync(string userName);
        bool HasUser(string userName);
        Task<bool> HasUserAsync(string userName);
        ReturnModel Delete(TUser user);
        Task<ReturnModel> DeleteAsync(TUser user);
        ReturnModel Delete(string userId);
        Task<ReturnModel> DeleteAsync(string userId);
        ReturnModel Update(TUser user);
        Task<ReturnModel> UpdateAsync(TUser user);
        bool CheckPassword(string userName, string password);
        Task<bool> CheckPasswordAsync(string userName, string password);
        bool CheckPassword(TUser user, string password);
        Task<bool> CheckPasswordAsync(TUser user, string password);
        string GetUserName(string userId);
        Task<string> GetUserNameAsync(string userId);
        bool AddToRole(string userId, string roleId);
        Task<bool> AddToRoleAsync(string userId, string roleId);
        bool AddToRoles(string userId, IList<string> roles);
        Task<bool> AddToRolesAsync(string userId, IList<string> roles);
        List<string> GetUserRoles(string userId);
        Task<List<string>> GetUserRolesAsync(string userId);
        List<string> GetUserRolesByName(string userName);
        Task<List<string>> GetUserRolesByNameAsync(string userName);
        bool IsInRole(string userId, string role);
        Task<bool> IsInRoleAsync(string userId, string role);
    }
}
