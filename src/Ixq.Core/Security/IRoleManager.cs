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
        where TRole : class
    {
        IQueryable<TRole> Roles { get; }
        ReturnModel Create(TRole role);
        Task<ReturnModel> CreateAsync(TRole role);
        TRole Find(string roleId);
        Task<TRole> FindAsync(string roleId);
        TRole FindByName(string roleName);
        Task<TRole> FindByNameAsync(string roleName);
        bool HasRole(string roleName);
        Task<bool> HasRoleAsync(string roleName);
        ReturnModel Delete(TRole role);
        Task<ReturnModel> DeleteAsync(TRole role);
        ReturnModel Delete(string roleId);
        Task<ReturnModel> DeleteAsync(string roleId);
        ReturnModel Update(TRole role);
        Task<ReturnModel> UpdateAsync(TRole role);
        string GetRoleName(string roleId);
        Task<string> GetRoleNameAsync(string roleId);

    }
}
