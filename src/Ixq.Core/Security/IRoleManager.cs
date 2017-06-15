using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.DependencyInjection;
using Ixq.Core.Entity;
using Ixq.Extensions.ObjectModel;

namespace Ixq.Core.Security
{
    public interface IRoleManager<TRole> : IScopeDependency
        where TRole : class , IEntity<Guid>, new()
    {
        IQueryable<TRole> Roles { get; }
        ReturnModel Create(TRole role);
        Task<ReturnModel> CreateAsync(TRole role);
        TRole Find(Guid roleId);
        Task<TRole> FindAsync(Guid roleId);
        TRole Find(string roleName);
        Task<TRole> FindAsync(string roleName);
        bool HasRole(string roleName);
        Task<bool> HasRoleAsync(string roleName);
        ReturnModel Delete(TRole role);
        Task<ReturnModel> DeleteAsync(TRole role);
        ReturnModel Delete(Guid roleId);
        Task<ReturnModel> DeleteAsync(Guid roleId);
        ReturnModel Update(TRole role);
        Task<ReturnModel> UpdateAsync(TRole role);
        string GetRoleName(Guid roleId);
        Task<string> GetRoleNameAsync(Guid roleId);

    }
}
