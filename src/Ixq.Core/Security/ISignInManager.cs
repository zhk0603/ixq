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
    public interface ISignInManager<TUser> : IScopeDependency
        where TUser :class , IEntity<Guid>, new()
    {
        TUser CurrentUser { get; }
        ReturnModel SignIn(TUser user, bool rememberBrowser);
        Task<ReturnModel> SignInAsync(TUser user, bool rememberBrowser);
        void SignOut();
    }
}
