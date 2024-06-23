using Code.ECS.Status.Geo.Defend;
using Code.ScriptableObjects.Status_Effect;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.ECS.Status.Geo.Defending
{
    internal class InitDefendingSystem : IEcsRunSystem
    {
        private readonly EcsFilter<DefendComponent, DefendTriggerComponent> _filter;
        private readonly EffectConfig _effectConfig;

        private DefendEffectData Effect => _effectConfig.DefendingData as DefendEffectData;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(i);
                ref DefendTriggerComponent trigger = ref _filter.Get2(i);

                ref DefendingComponent component = ref entity.Get<DefendingComponent>();

                component.ObjTransform = trigger.Collider.gameObject.transform;
                component.Particle = Effect.Particle;
                component.DefensePoints = Effect.DefensePoints;
                component.Duration = Effect.Duration;

                MaterialSetup(ref component, Effect.Materials);

                entity.Del<DefendTriggerComponent>();
            }
        }

        private static void MaterialSetup(ref DefendingComponent component, Material[] materials)
        {
            component.Materials = materials;
            MeshRenderer meshRenderer = component.ObjTransform.GetComponent<MeshRenderer>();
            component.PreviousMaterials = meshRenderer.materials;
            meshRenderer.materials = component.Materials;
        }
    }
}
