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
        where TUser : class, IEntity<Guid>, new()
    {
        IQueryable<TUser> Users { get; }
        ReturnModel Create(TUser user);
        Task<ReturnModel> CreateAsync(TUser user);
        TUser Find(Guid userId);
        Task<TUser> FindAsync(Guid userId);
        TUser Find(string userName, string password);
        Task<TUser> FindAsync(string userName, string password);
        TUser Find(string userName);
        Task<TUser> FindAsync(string userName);
        bool HasUser(string userName);
        Task<bool> HasUserAsync(string userName);
        ReturnModel Delete(TUser user);
        Task<ReturnModel> DeleteAsync(TUser user);
        ReturnModel Delete(Guid userId);
        Task<ReturnModel> DeleteAsync(Guid userId);
        ReturnModel Update(TUser user);
        Task<ReturnModel> UpdateAsync(TUser user);
        bool CheckPassword(string userName, string password);
        Task<bool> CheckPasswordAsync(string userName, string password);
        bool CheckPassword(TUser user, string password);
        Task<bool> CheckPasswordAsync(TUser user, string password);
        string GetUserName(Guid userId);
        Task<string> GetUserNameAsync(Guid userId);
        bool AddToRole(Guid userId, Guid roleId);
        Task<bool> AddToRoleAsync(Guid userId, Guid roleId);
        bool AddToRole(Guid userId, IList<Guid> roles);
        Task<bool> AddToRoleAsync(Guid userId, IList<Guid> roles);
        List<string> GetUserRoles(Guid userId);
        Task<List<string>> GetUserRolesAsync(Guid userId);
        bool IsInRole(Guid userId, string role);
        Task<bool> IsInRoleAsync(Guid userId, string role);
    }
}
