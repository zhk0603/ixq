using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.Extensions
{
    public class AssemblyFinder
    {
        private static readonly IDictionary<string, Assembly[]> AssembliesesDict = new Dictionary<string, Assembly[]>();
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
            var assemblies = files.Select(Assembly.LoadFrom).Distinct().ToArray();
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
