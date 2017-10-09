using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ixq.Core.Dto;
using Ixq.Core.Entity;

namespace Ixq.Core.Mapper
{
    /// <summary>
    ///     映射集合。
    /// </summary>
    public class MapperCollection : IMapperCollection
    {
        private readonly List<MapperDescriptor> _descriptors = new List<MapperDescriptor>();

        public IEnumerator<MapperDescriptor> GetEnumerator()
        {
            return _descriptors.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(MapperDescriptor item)
        {
            _descriptors.Add(item);
        }

        public void Clear()
        {
            _descriptors.Clear();
        }

        public bool Contains(MapperDescriptor item)
        {
            return _descriptors.Contains(item);
        }

        public void CopyTo(MapperDescriptor[] array, int arrayIndex)
        {
            _descriptors.CopyTo(array, arrayIndex);
        }

        public bool Remove(MapperDescriptor item)
        {
            return _descriptors.Remove(item);
        }

        public int Count => _descriptors.Count;
        public bool IsReadOnly => false;

        public int IndexOf(MapperDescriptor item)
        {
            return _descriptors.IndexOf(item);
        }

        public void Insert(int index, MapperDescriptor item)
        {
            _descriptors.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _descriptors.RemoveAt(index);
        }

        public MapperDescriptor this[int index]
        {
            get => _descriptors[index];
            set => _descriptors[index] = value;
        }

        public void Init(Assembly[] assembly)
        {
            var sourceTypes = SelectMany(typeof(IDto<,>), assembly);
            var targetTypes = SelectMany(typeof(IEntity<>), assembly);
            if (sourceTypes.Length == 0 || targetTypes.Length == 0)
            {
                return;
            }
            foreach (var sType in sourceTypes)
            {
                var iType = sType.GetInterfaces();
                var targetType = _getTargetType(iType, targetTypes);
                if (targetType != null)
                {
                    Add(new MapperDescriptor(sType, targetType));
                }
            }
        }

        #region private method

        private Type[] SelectMany(Type t, Assembly[] assemblies)
        {
            return assemblies.SelectMany(assembly =>
                    assembly.GetTypes().Where(type =>
                        t.IsGenericAssignableFrom(type) && !type.IsAbstract))
                .Distinct().ToArray();
        }

        /// <summary>
        ///     获取目标类型
        /// </summary>
        /// <param name="interfaceTypes"></param>
        /// <param name="targetTypes"></param>
        /// <returns></returns>
        private Type _getTargetType(Type[] interfaceTypes, Type[] targetTypes)
        {
            Type targetType = null;
            foreach (var iType in interfaceTypes)
            {
                if (targetType != null)
                {
                    break;
                }
                targetType = iType.GenericTypeArguments.FirstOrDefault(x => targetTypes.Contains(x));
            }
            return targetType;
        }

        #endregion
    }
}