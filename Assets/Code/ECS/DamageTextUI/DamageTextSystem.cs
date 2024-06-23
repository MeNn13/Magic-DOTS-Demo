using Code.ECS.EntityRef;
using Code.ECS.EntityRef.Mono;
using Code.ECS.Health;
using Leopotam.Ecs;
using Plugins.Custom;
using TMPro;
using UnityEngine;

namespace Code.ECS.DamageTextUI
{
    public class DamageTextSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsFilter<HealthComponent> _initFilter;
        
        private readonly EcsFilter<HealthComponent, DamageTextStartTriggerComponent>
            .Exclude<DamageTextEndTriggerComponent> _startFilter;
        
        private readonly EcsFilter<DamageTextEndTriggerComponent>
            .Exclude<DamageTextStartTriggerComponent> _endFilter;

        private CustomPool<TextMeshPro> _pool;

        public void Init()
        {
            foreach (var i in _initFilter)
            {
                ref HealthComponent damageText = ref _initFilter.Get1(i);

                TryCreateTextPool(damageText.damageText);
            }

            void TryCreateTextPool(TextMeshPro text)
            {
                _pool ??= new CustomPool<TextMeshPro>(text, 50, "Damage Text UI");
            }
        }
        
        public void Run()
        {
            foreach (var i in _startFilter)
            {
                ref EcsEntity entity = ref _startFilter.GetEntity(i);
                ref DamageTextStartTriggerComponent component = ref _startFilter.Get2(i);

                float damage = component.Damage;
                if (damage < 1)
                    return;

                TextMeshPro damageText = _pool.Get();
                damageText.text = damage.ToString();
                Transform spawnTransform = entity.Get<EntityReferenceComponent>().Reference.transform;
                Vector3 spawnPoint = spawnTransform.position;
                spawnPoint.x = Random.Range(spawnPoint.x - 1, spawnPoint.x + 1);
                spawnPoint.y += Random.Range(2f, 2.5f);
                damageText.transform.position = spawnPoint;
                
                entity.Del<DamageTextStartTriggerComponent>();
            }

            foreach (var i in _endFilter)
            {
                ref EcsEntity entity = ref _endFilter.GetEntity(i);
                ref DamageTextEndTriggerComponent component = ref _endFilter.Get1(i);

                _pool.Release(component.Text);
            }
        }
    }
}
