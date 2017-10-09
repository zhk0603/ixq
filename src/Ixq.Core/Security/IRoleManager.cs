using System.Linq;
using System.Threading.Tasks;
using Ixq.Core.DependencyInjection;
using Ixq.Extensions.ObjectModel;

namespace Ixq.Core.Security
{
    /// <summary>
    ///     角色管理器。
    /// </summary>
    /// <typeparam name="TRole"></typeparam>
    public interface IRoleManager<TRole> : IScopeDependency
        where TRole : class
    {
        /// <summary>
        ///     获取所有的系统角色。
        /// </summary>
        IQueryable<TRole> Roles { get; }

        /// <summary>
        ///     创建一个角色。
        /// </summary>
        /// <param name="role">尚未添加至上下文的角色。</param>
        /// <returns></returns>
        ReturnModel Create(TRole role);

        /// <summary>
        ///     异步创建一个角色。
        /// </summary>
        /// <param name="role">尚未添加至上下文的角色。</param>
        /// <returns></returns>
        Task<ReturnModel> CreateAsync(TRole role);

        /// <summary>
        ///     根据角色id，查找角色对象。
        /// </summary>
        /// <param name="roleId">角色id.</param>
        /// <returns></returns>
        TRole Find(string roleId);

        /// <summary>
        ///     异步根据角色id，查找角色对象。
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns></returns>
        Task<TRole> FindAsync(string roleId);

        /// <summary>
        ///     根据角色名字，查找角色对象。
        /// </summary>
        /// <param name="roleName">角色名字。</param>
        /// <returns></returns>
        TRole FindByName(string roleName);

        /// <summary>
        ///     异步根据角色名字，查找角色对象。
        /// </summary>
        /// <param name="roleName">角色名字。</param>
        /// <returns></returns>
        Task<TRole> FindByNameAsync(string roleName);

        /// <summary>
        ///     根据角色名字判断上下文是否存在。
        /// </summary>
        /// <param name="roleName">角色名字。</param>
        /// <returns></returns>
        bool HasRole(string roleName);

        /// <summary>
        ///     异步根据角色名字判断上下文是否存在。
        /// </summary>
        /// <param name="roleName">角色名字。</param>
        /// <returns></returns>
        Task<bool> HasRoleAsync(string roleName);

        /// <summary>
        ///     删除一个角色。
        /// </summary>
        /// <param name="role">需要删除的角色。</param>
        /// <returns></returns>
        ReturnModel Delete(TRole role);

        /// <summary>
        ///     异步删除一个角色。
        /// </summary>
        /// <param name="role">需要删除的角色。</param>
        /// <returns></returns>
        Task<ReturnModel> DeleteAsync(TRole role);

        /// <summary>
        ///     根据角色Id,删除一个角色。
        /// </summary>
        /// <param name="roleId">角色Id。</param>
        /// <returns></returns>
        ReturnModel Delete(string roleId);

        /// <summary>
        ///     异步根据角色Id,删除一个角色。
        /// </summary>
        /// <param name="roleId">角色Id。</param>
        /// <returns></returns>
        Task<ReturnModel> DeleteAsync(string roleId);

        /// <summary>
        ///     更新一个角色。
        /// </summary>
        /// <param name="role">修改过的角色。</param>
        /// <returns></returns>
        ReturnModel Update(TRole role);

        /// <summary>
        ///     异步更新一个角色。
        /// </summary>
        /// <param name="role">修改过的角色。</param>
        /// <returns></returns>
        Task<ReturnModel> UpdateAsync(TRole role);

        /// <summary>
        ///     根据角色id，获取角色名字。
        /// </summary>
        /// <param name="roleId">角色id。</param>
        /// <returns></returns>
        string GetRoleName(string roleId);

        /// <summary>
        ///     异步根据角色id，获取角色名字。
        /// </summary>
        /// <param name="roleId">角色id。</param>
        /// <returns></returns>
        Task<string> GetRoleNameAsync(string roleId);
    }
}