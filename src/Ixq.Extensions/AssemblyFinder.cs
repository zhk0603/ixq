using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Ixq.Extensions
{
    public class AssemblyFinder
    {
        private static readonly IDictionary<string, Assembly[]> AssembliesesDict = new Dictionary<string, Assembly[]>();

        /// <summary>
        ///     在加载程序集时需要过滤的程序集，加快系统启动速度。
        /// </summary>
        public static string[] IgnoreAssembly =
        {
            "Antlr3.Runtime, Version=3.4.1.9004, Culture=neutral, PublicKeyToken=eb42632606e9261f",
            "Microsoft.CodeDom.Providers.DotNetCompilerPlatform, Version=1.0.3.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35",
            "Newtonsoft.Json, Version=10.0.0.0, Culture=neutral, PublicKeyToken=30ad4fe6b2a6aeed",
            "Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35",
            "Microsoft.AspNet.Identity.EntityFramework, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35",
            "Microsoft.AspNet.Identity.Owin, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35",
            "System.Web.Mvc, Version=5.2.3.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35",
            "System.Web.Optimization, Version=1.1.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35",
            "System.Web.Razor, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35",
            "System.Web.WebPages.Deployment, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35",
            "System.Web.WebPages, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35",
            "System.Web.WebPages.Razor, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35",
            "Microsoft.Owin, Version=3.0.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35",
            "Microsoft.Owin.Host.SystemWeb, Version=3.0.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35",
            "Microsoft.Owin.Security.Cookies, Version=3.0.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35",
            "Microsoft.Owin.Security, Version=3.0.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35",
            "Microsoft.Owin.Security.OAuth, Version=3.0.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35",
            "Owin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=f0ebd12fd5e55cc5",
            "System.Web.Helpers, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35",
            "WebGrease, Version=1.6.5135.21930, Culture=neutral, PublicKeyToken=31bf3856ad364e35",
            "Microsoft.Web.Infrastructure, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
        };

        private readonly string _path;

        /// <summary>
        ///     初始化一个<see cref="AssemblyFinder" />类型的新实例
        /// </summary>
        public AssemblyFinder()
            : this(GetBinPath())
        {
        }

        /// <summary>
        ///     初始化一个<see cref="AssemblyFinder" />类型的新实例
        /// </summary>
        public AssemblyFinder(string path)
        {
            _path = path;
        }

        /// <summary>
        ///     查找所有项
        /// </summary>
        /// <returns></returns>
        public Assembly[] FindAll()
        {
            if (AssembliesesDict.ContainsKey(_path))
            {
                return AssembliesesDict[_path];
            }
            var files = Directory.GetFiles(_path, "*.dll", SearchOption.TopDirectoryOnly)
                .Concat(Directory.GetFiles(_path, "*.exe", SearchOption.TopDirectoryOnly))
                .ToArray();
            var assemblies =
                files.Select(Assembly.LoadFrom).Distinct().Where(x => !IgnoreAssembly.Contains(x.FullName)).ToArray();
            AssembliesesDict.Add(_path, assemblies);
            return assemblies;
        }

        /// <summary>
        ///     查找指定条件的项
        /// </summary>
        /// <param name="predicate">筛选条件</param>
        /// <returns></returns>
        public Assembly[] Find(Func<Assembly, bool> predicate)
        {
            return FindAll().Where(predicate).ToArray();
        }

        private static string GetBinPath()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            return path == Environment.CurrentDirectory + "\\" ? path : Path.Combine(path, "bin");
        }
    }
}