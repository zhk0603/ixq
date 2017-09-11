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
    /// <summary>
    ///     用户管理器。
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    public interface IUserManager<TUser> : IScopeDependency
        where TUser : class
    {
        /// <summary>
        ///     获取所有的系统用户。
        /// </summary>
        IQueryable<TUser> Users { get; }
        /// <summary>
        ///     创建一个用户。
        /// </summary>
        /// <param name="user">尚未添加至上下文的用户。</param>
        /// <returns></returns>
        ReturnModel Create(TUser user);
        /// <summary>
        ///     异步创建一个用户。
        /// </summary>
        /// <param name="user">尚未添加至上下文的用户。</param>
        /// <returns></returns>
        Task<ReturnModel> CreateAsync(TUser user);
        /// <summary>
        ///     根据用户id，查找用户对象。
        /// </summary>
        /// <param name="userId">用户id.</param>
        /// <returns></returns>
        TUser Find(string userId);
        /// <summary>
        ///     异步根据用户id，查找用户对象。
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        Task<TUser> FindAsync(string userId);
        /// <summary>
        ///     根据用户名与密码查找用户。
        /// </summary>
        /// <param name="userName">用户名。</param>
        /// <param name="password">密码。</param>
        /// <returns></returns>
        TUser Find(string userName, string password);
        /// <summary>
        ///     异步根据用户名与密码查找用户。
        /// </summary>
        /// <param name="userName">用户名。</param>
        /// <param name="password">密码。</param>
        /// <returns></returns>
        Task<TUser> FindAsync(string userName, string password);
        /// <summary>
        ///     根据用户名，查找用户。
        /// </summary>
        /// <param name="userName">用户名。</param>
        /// <returns></returns>
        TUser FindByName(string userName);
        /// <summary>
        ///     异步根据用户名，查找用户。
        /// </summary>
        /// <param name="userName">用户名。</param>
        /// <returns></returns>
        Task<TUser> FindByNameAsync(string userName);
        /// <summary>
        ///     根据用户名字判断上下文是否存在。
        /// </summary>
        /// <param name="userName">用户名。</param>
        /// <returns></returns>
        bool HasUser(string userName);
        /// <summary>
        ///     异步根据用户名字判断上下文是否存在。
        /// </summary>
        /// <param name="userName">用户名。</param>
        /// <returns></returns>
        Task<bool> HasUserAsync(string userName);
        /// <summary>
        ///     删除一个用户。
        /// </summary>
        /// <param name="user">需要删除的用户。</param>
        /// <returns></returns>
        ReturnModel Delete(TUser user);
        /// <summary>
        ///     异步删除一个用户。
        /// </summary>
        /// <param name="user">需要删除的用户。</param>
        /// <returns></returns>
        Task<ReturnModel> DeleteAsync(TUser user);
        /// <summary>
        ///     根据用户Id,删除一个用户。
        /// </summary>
        /// <param name="userId">用户Id.</param>
        /// <returns></returns>
        ReturnModel Delete(string userId);
        /// <summary>
        ///     异步根据用户Id,删除一个用户。
        /// </summary>
        /// <param name="userId">用户Id.</param>
        /// <returns></returns>
        Task<ReturnModel> DeleteAsync(string userId);
        /// <summary>
        ///     更新一个用户。
        /// </summary>
        /// <param name="user">修改过的用户。</param>
        /// <returns></returns>
        ReturnModel Update(TUser user);
        /// <summary>
        ///     异步更新一个用户。
        /// </summary>
        /// <param name="user">修改过的用户。</param>
        /// <returns></returns>
        Task<ReturnModel> UpdateAsync(TUser user);
        /// <summary>
        ///     根据用户名与密码，确认用户密码是否匹配。
        /// </summary>
        /// <param name="userName">用户名。</param>
        /// <param name="password">密码。</param>
        /// <returns></returns>
        bool CheckPassword(string userName, string password);
        /// <summary>
        ///     异步根据用户名与密码，确认用户密码是否匹配。
        /// </summary>
        /// <param name="userName">用户名。</param>
        /// <param name="password">密码。</param>
        /// <returns></returns>
        Task<bool> CheckPasswordAsync(string userName, string password);
        /// <summary>
        ///     根据用户与密码，确认用户密码是否匹配。
        /// </summary>
        /// <param name="user">用户。</param>
        /// <param name="password">密码。</param>
        /// <returns></returns>
        bool CheckPassword(TUser user, string password);
        /// <summary>
        ///     异步根据用户与密码，确认用户密码是否匹配。
        /// </summary>
        /// <param name="user">用户。</param>
        /// <param name="password">密码。</param>
        /// <returns></returns>
        Task<bool> CheckPasswordAsync(TUser user, string password);
        /// <summary>
        ///     根据用户Id，获取用户名。
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        string GetUserName(string userId);
        /// <summary>
        ///     异步根据用户Id，获取用户名。
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        Task<string> GetUserNameAsync(string userId);
        /// <summary>
        ///     为用户添加一个角色。
        /// </summary>
        /// <param name="userId">用户Id.</param>
        /// <param name="roleId">角色Id.</param>
        /// <returns></returns>
        bool AddToRole(string userId, string roleId);
        /// <summary>
        ///     异步为用户添加一个角色。
        /// </summary>
        /// <param name="userId">用户Id.</param>
        /// <param name="roleId">角色Id.</param>
        /// <returns></returns>
        Task<bool> AddToRoleAsync(string userId, string roleId);
        /// <summary>
        ///     为用户添加角色。
        /// </summary>
        /// <param name="userId">用户Id.</param>
        /// <param name="roles">角色。</param>
        /// <returns></returns>
        bool AddToRoles(string userId, IList<string> roles);
        /// <summary>
        ///     异步为用户添加角色。
        /// </summary>
        /// <param name="userId">用户Id.</param>
        /// <param name="roles">角色。</param>
        /// <returns></returns>
        Task<bool> AddToRolesAsync(string userId, IList<string> roles);
        /// <summary>
        ///     根据用户Id,获取用户的所有角色。
        /// </summary>
        /// <param name="userId">用户Id.</param>
        /// <returns></returns>
        List<string> GetUserRoles(string userId);
        /// <summary>
        ///     异步根据用户Id,获取用户的所有角色。
        /// </summary>
        /// <param name="userId">用户Id.</param>
        /// <returns></returns>
        Task<List<string>> GetUserRolesAsync(string userId);
        /// <summary>
        ///     根据用户名，获取用户所有角色。
        /// </summary>
        /// <param name="userName">用户名。</param>
        /// <returns></returns>
        List<string> GetUserRolesByName(string userName);
        /// <summary>
        ///     异步根据用户名，获取用户所有角色。
        /// </summary>
        /// <param name="userName">用户名。</param>
        /// <returns></returns>
        Task<List<string>> GetUserRolesByNameAsync(string userName);
        /// <summary>
        ///     判断用户是否拥有某个角色。
        /// </summary>
        /// <param name="userId">用户Id.</param>
        /// <param name="role">角色名。</param>
        /// <returns></returns>
        bool IsInRole(string userId, string role);
        /// <summary>
        ///     异步判断用户是否拥有某个角色。
        /// </summary>
        /// <param name="userId">用户Id.</param>
        /// <param name="role">角色名。</param>
        /// <returns></returns>
        Task<bool> IsInRoleAsync(string userId, string role);
    }
}
