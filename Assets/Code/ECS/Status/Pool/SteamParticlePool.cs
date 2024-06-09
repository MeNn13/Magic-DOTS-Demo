using UnityEngine;

namespace Code.ECS.Status.Pool
{
    internal class SteamParticlePool : ParticlePool
    {
        public SteamParticlePool(ParticleSystem prefab, int prewarmObjectCount, string poolName) 
            : base(prefab, prewarmObjectCount, poolName)
        {
        }
    }
}
