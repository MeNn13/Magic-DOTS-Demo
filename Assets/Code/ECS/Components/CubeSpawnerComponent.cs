using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Code.ECS.Components
{
    public struct CubeSpawnerComponent : IComponentData
    {
        public Entity Prefab;
        public float3 SpawnPosition;
        public float NextSpawnRate;
        public float SpawnRate;
    }
}
