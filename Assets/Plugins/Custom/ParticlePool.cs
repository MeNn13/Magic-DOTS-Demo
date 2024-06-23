using UnityEngine;
using UnityEngine.Pool;

namespace Plugins.Custom
{
    public class ParticlePool
    {
        private readonly ObjectPool<ParticleSystem> _pool;
        private readonly ParticleSystem _particle;
        private readonly GameObject _parent;

        protected ParticlePool(ParticleSystem particle, int prewarmObjectCount, string poolName)
        {
            _particle = particle;
            _parent = new GameObject(poolName);
            _pool = new ObjectPool<ParticleSystem>(OnCreate, OnGet, OnRelease, OnDestroy, false, prewarmObjectCount);
        }

        public ParticleSystem Get()
        {
            ParticleSystem particle = _pool.Get();
            return particle;
        }

        public void Release(ParticleSystem particle)
        {
            particle.transform.parent = _parent.transform;
            _pool.Release(particle);
        }

        private ParticleSystem OnCreate()
        {
            return Object.Instantiate(_particle, _parent.transform);
        }

        private void OnGet(ParticleSystem particle)
        {
            particle.gameObject.SetActive(true);
            particle.Play();
        }

        private void OnRelease(ParticleSystem particle)
        {
            particle.gameObject.SetActive(false);
            particle.Stop();
        }

        private void OnDestroy(ParticleSystem particle)
        {
            Object.Destroy(particle);
        }
    }
}
