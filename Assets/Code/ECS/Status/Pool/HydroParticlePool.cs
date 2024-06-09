using UnityEngine;

namespace Code.ECS.Status.Pool
{
    internal class HydroParticlePool : ParticlePool
    {
        public HydroParticlePool(ParticleSystem prefab, int prewarmObjectCount, string poolName) 
            : base(prefab, prewarmObjectCount, poolName)
        {
        }
    }
}
