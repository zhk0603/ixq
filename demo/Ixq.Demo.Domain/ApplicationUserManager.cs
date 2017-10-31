using System;
using System.Reflection;
using Autofac.Integration.Mvc;
using ixq.Demo.DbContext;
using Ixq.Demo.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Ixq.Core.DependencyInjection;
using Ixq.Core.DependencyInjection.Extensions;
using Ixq.DependencyInjection.Autofac;
using Ixq.Security.Identity;

namespace Ixq.Demo.Domain
{
    public class ApplicationUserManager : AppUserManager<ApplicationUser>
    {
        public ApplicationUserManager(IAppUserStore<ApplicationUser> store) : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
            IOwinContext context)
        {
            var manager = new ApplicationUserManager(new AppUserStore<ApplicationUser>(context.Get<DataContext>()));
            // 配置用户名的验证逻辑
            manager.UserValidator = new AppUserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = false
            };

            // 配置密码的验证逻辑
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // 配置用户锁定默认值
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            return manager;
        }
    }
}