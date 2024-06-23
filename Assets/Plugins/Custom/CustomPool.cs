using System.Collections.Generic;
using UnityEngine;

namespace Plugins.Custom
{
    public class CustomPool<T> where T : MonoBehaviour
    {
        private readonly List<T> _pool = new();
        private readonly T _prefab;
        private readonly GameObject _parent;

        public CustomPool(T prefab, int prewarmObjectCount, string poolName)
        {
            _prefab = prefab;
            _parent = new GameObject(poolName);
            AddObjectInPool(prewarmObjectCount);
        }

        private void AddObjectInPool(int count)
        {
            for (int i = 0; i < count; i++)
            {
                T obj = OnCreate();
                obj.gameObject.SetActive(false);
                _pool.Add(obj);
            }
        }

        public T Get()
        {
            foreach (var obj in _pool)
                if (!obj.gameObject.activeSelf)
                {
                    obj.gameObject.SetActive(true);
                    return obj;
                }

            return OnCreate();
        }

        public void Release(T obj)
        {
            obj.gameObject.transform.SetParent(_parent.transform);
            obj.gameObject.SetActive(false);
        }

        private T OnCreate()
        {
            return Object.Instantiate(_prefab, _parent.transform);
        }
    }
}
