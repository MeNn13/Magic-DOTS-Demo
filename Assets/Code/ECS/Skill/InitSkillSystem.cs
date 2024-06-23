using System;
using Code.ECS.Attack;
using Code.ECS.EntityRef.Mono;
using Code.ECS.Input;
using Code.ECS.Skill.Cooldown;
using Code.ECS.Skill.Geo;
using Code.ECS.Skill.Hydro;
using Code.ECS.Skill.Mono;
using Code.ECS.Skill.Pyro;
using Code.ECS.Skill.Vento;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.ECS.Skill
{
    internal class InitSkillSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly EcsFilter<InputComponent, AttackComponent>
            .Exclude<SkillCooldownComponent> _filter;

        private readonly SkillUI _ui;
        private readonly SkillsCircle _skillsCircle;
        private EcsEntity _skillContainer;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(i);
                ref InputComponent input = ref _filter.Get1(i);
                ref AttackComponent attackComponent = ref _filter.Get2(i);

                SetIconUIValue(input);

                TryCreateSkillContainer();
                TryAddSkillComponent(input);

                if (input.isAttacking
                    && !attackComponent.active
                    && _skillContainer.Has<SkillComponent>())
                    SkillComplete(ref entity, attackComponent);
            }
        }

        private void TryAddSkillComponent(InputComponent input)
        {
            if (input.isAttacking)
                return;

            AddSkillComponent<PyroComponent>(input.isPyro, _skillsCircle.SetFireCircle);
            AddSkillComponent<HydroComponent>(input.isHydro, _skillsCircle.SetHydroCircle);
            AddSkillComponent<VentoComponent>(input.isVento, _skillsCircle.SetVentoCircle);
            AddSkillComponent<GeoComponent>(input.isGeo, _skillsCircle.SetGeoCircle);
        }

        private void AddSkillComponent<T>(bool input, Action<bool> circleUiAction)
            where T : struct
        {
            if (input)
            {
                _skillContainer.Get<T>();
                circleUiAction.Invoke(true);
                _skillContainer.Get<SkillComponent>();
            }
        }

        private void TryCreateSkillContainer()
        {
            if (_skillContainer != EcsEntity.Null)
                return;

            _skillContainer = _world.NewEntity();
            _skillContainer.Get<SkillContainerComponent>();
        }

        private void SkillComplete(ref EcsEntity characterEntity, AttackComponent attackComponent)
        {
            GameObject skill = new("Skill");
            skill.transform.parent = attackComponent.hand;
            skill.transform.localPosition = Vector3.zero;
            skill.transform.localRotation = Quaternion.identity;
            
            EcsEntity skillEntity = _world.NewEntity();
            _skillContainer.MoveTo(skillEntity);

            ref SkillComponent component = ref skillEntity.Get<SkillComponent>();
            component.Transform = skill.transform;

            skill.AddComponent<EntityReference>().Entity = skillEntity;

            skillEntity.Del<SkillContainerComponent>();
            _skillContainer = EcsEntity.Null;
            characterEntity.Get<SkillCooldownComponent>().Cooldown = .5f;

            _skillsCircle.SetAllCircles(false);
        }

        private void SetIconUIValue(InputComponent input)
        {
            _ui.PyroPres(input.isPyro);
            _ui.HydroPres(input.isHydro);
            _ui.VentoPres(input.isVento);
            _ui.GeoPres(input.isGeo);
        }
    }
}
