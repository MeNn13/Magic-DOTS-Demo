using UnityEngine;

namespace Assets.Code.ECS.Skills.Pool
{
    internal class BurnParticlePool : ParticlePool
    {
        public BurnParticlePool(ParticleSystem prefab, int prewarmObjectCount, string poolName) 
            : base(prefab, prewarmObjectCount, poolName)
        {

        }
    }
}
