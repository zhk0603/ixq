using System.Collections;
using System.Collections.Generic;

namespace Ixq.Core.Mapper
{
    public abstract class MapperCollection : IMapperCollection
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
            get
            {
                return _descriptors[index];
            }
            set
            {
                _descriptors[index] = value;
            }
        }
    }
}
