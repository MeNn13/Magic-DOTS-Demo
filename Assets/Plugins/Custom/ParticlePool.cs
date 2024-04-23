using UnityEngine;
using UnityEngine.Pool;
using static UnityEngine.ParticleSystem;

public class ParticlePool
{
    private ObjectPool<ParticleSystem> _pool;
    private ParticleSystem _prefab;
    private readonly string _poolName;
    private readonly GameObject _parent;

    public ParticlePool(ParticleSystem prefab, int prewarmObjectCount, string poolName)
    {
        _prefab = prefab;
        _poolName = poolName;
        _parent = new(_poolName);
        _pool = new ObjectPool<ParticleSystem>(OnCreate, OnGet, OnRelease, OnDestroy, false, prewarmObjectCount);
    }

    public ParticleSystem Get()
    {
        ParticleSystem obj = _pool.Get();
        return obj;
    }

    public void Release(ParticleSystem obj)
    {
        obj.transform.parent = _parent.transform;
        _pool.Release(obj);
    }

    private ParticleSystem OnCreate()
    {
        return Object.Instantiate(_prefab, _parent.transform);
    }

    private void OnGet(ParticleSystem particle)
    {
        particle.Play();
    }

    private void OnRelease(ParticleSystem particle)
    {
        particle.Stop();
    }

    private void OnDestroy(ParticleSystem particle)
    {
        Object.Destroy(particle);
    }
}
