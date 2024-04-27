using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Code.ECS.Status.Geo.Defending
{
    internal class InitDefendingSystem : IEcsRunSystem
    {
        private readonly EcsFilter<DefendComponent, DefendTriggerComponent> _filter;
        private readonly EffectConfig _effectConfig;

        private DefendEffectData _effect => _effectConfig.DefendingData as DefendEffectData;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(i);
                ref DefendTriggerComponent trigger = ref _filter.Get2(i);

                ref DefendingComponent component = ref entity.Get<DefendingComponent>();

                component.objTransform = trigger.collider.gameObject.transform;
                component.particle = _effect.Particle;
                component.material = _effect.Material;
                component.defensePoints = _effect.DefensePoints;
                component.duration = _effect.Duration;

                MaterialSetup(ref component);

                entity.Del<DefendTriggerComponent>();
            }
        }

        private static void MaterialSetup(ref DefendingComponent component)
        {
            MeshRenderer meshRenderer = component.objTransform.GetComponent<MeshRenderer>();
            component.previousMaterials = meshRenderer.materials;
            meshRenderer.material = component.material;
        }
    }
}
