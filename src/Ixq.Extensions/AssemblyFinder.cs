using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Ixq.Extensions
{
    public class AssemblyFinder
    {
        private static readonly IDictionary<string, Assembly[]> AssembliesesDict = new Dictionary<string, Assembly[]>();

        private readonly string _path;
        private string _assemblySkipLoadingPattern = "^System|^mscorlib|^Microsoft|^AjaxControlToolkit|^Antlr3|^Autofac|^AutoMapper|^Castle|^ComponentArt|^CppCodeProvider|^DotNetOpenAuth|^EntityFramework|^EPPlus|^FluentValidation|^ImageResizer|^itextsharp|^log4net|^MaxMind|^MbUnit|^MiniProfiler|^Mono.Math|^MvcContrib|^Newtonsoft|^NHibernate|^nunit|^Org.Mentalis|^PerlRegex|^QuickGraph|^Recaptcha|^Remotion|^RestSharp|^Rhino|^Telerik|^Iesi|^TestDriven|^TestFu|^UserAgentStringLibrary|^VJSharpCodeProvider|^WebActivator|^WebDev|^WebGrease|^Owin";
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
        ///     获取或设置不需要加载的dll.
        /// </summary>
        public string AssemblySkipLoadingPattern
        {
            get => _assemblySkipLoadingPattern;
            set => _assemblySkipLoadingPattern = value;
        }

        /// <summary>
        ///     查找所有项
        /// </summary>
        /// <returns></returns>
        public Assembly[] FindAll()
        {
            if (AssembliesesDict.ContainsKey(_path))
                return AssembliesesDict[_path];
            var files = Directory.GetFiles(_path, "*.dll", SearchOption.TopDirectoryOnly)
                .Concat(Directory.GetFiles(_path, "*.exe", SearchOption.TopDirectoryOnly))
                .ToArray();
            var assemblies =
                files.Select(Assembly.LoadFrom).Distinct()
                    .Where(x => Matches(x.FullName, AssemblySkipLoadingPattern))
                    .ToArray();
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

        protected virtual bool Matches(string assemblyFullName, string pattern)
        {
            return !Regex.IsMatch(assemblyFullName, pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        private static string GetBinPath()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            return path == Environment.CurrentDirectory + "\\" ? path : Path.Combine(path, "bin");
        }
    }
}