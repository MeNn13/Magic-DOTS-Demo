using Assets.Code.ECS.EntityRef;
using Assets.Code.ECS.Input;
using Assets.Code.ECS.Skill.Mono;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Code.ECS.Skill
{
    internal class InitSkillSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly EcsFilter<InputComponent, AttackComponent>
            .Exclude<SkillCooldownComponent> _filter;

        private readonly SkillUI _ui;

        private EcsEntity _skillContainer;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(i);
                ref InputComponent input = ref _filter.Get1(i);
                ref AttackComponent attack = ref _filter.Get2(i);

                TryCreateSkillContainer();

                if (!input.isAttacking)
                {
                    SetUIValue(input);

                    AddStatusComponent(ref input, ref _skillContainer);
                }

                if (input.isAttacking)
                    SkillComplete(ref entity);
            }
        }

        private void TryCreateSkillContainer()
        {
            if (_skillContainer == EcsEntity.Null)
            {
                _skillContainer = _world.NewEntity();
                _skillContainer.Get<SkillContainerComponent>();
                _skillContainer.Get<SkillComponent>();
            }
        }

        private void SkillComplete(ref EcsEntity entity)
        {
            GameObject skill = new();
            EcsEntity skillEntity = _world.NewEntity();
            _skillContainer.MoveTo(skillEntity);

            ref SkillComponent component = ref skillEntity.Get<SkillComponent>();
            component.duration = 3f;
            component.transform = skill.transform;

            skill.AddComponent<EntityReference>().Entity = skillEntity;

            skillEntity.Del<SkillContainerComponent>();
            _skillContainer = EcsEntity.Null;
            entity.Get<SkillCooldownComponent>().cooldown = .5f;
        }

        private void SetUIValue(InputComponent input)
        {
            _ui.PyroPres(input.isPyro);
            _ui.HydroPres(input.isHydro);
            _ui.VentoPres(input.isVento);
            _ui.GeoPres(input.isGeo);
        }

        private void AddStatusComponent(ref InputComponent input, ref EcsEntity entity)
        {
            if (input.isPyro && !entity.Has<BurningComponent>())
                entity.Get<BurningComponent>();
            else if (input.isHydro && !entity.Has<SoggyComponent>())
                entity.Get<SoggyComponent>();
        }
    }
}
