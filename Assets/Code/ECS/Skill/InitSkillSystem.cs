using Assets.Code.ECS.EntityRef;
using Code.ECS.Input;
using Code.ECS.Skill.Mono;
using Code.ECS.Skill.SkillsComponent;
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
        private const int MAXComponentCount = 5;
        private EcsEntity _skillContainer;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(i);
                ref InputComponent input = ref _filter.Get1(i);

                SetIconUIValue(input);
                TryCreateSkillContainer();
                TryAddSkillComponent(input);

                if (input.isAttacking)
                    SkillComplete(ref entity);
            }
        }
        private void TryAddSkillComponent(InputComponent input)
        {
            if (input.isAttacking || GetSkillCount() >= MAXComponentCount)
                return;

            AddSkillComponent(ref input, ref _skillContainer);
        }
        private int GetSkillCount()
        {
            var skillComponent = _skillContainer.Get<SkillComponent>();

            return skillComponent.PyroCount + skillComponent.HydroCount + skillComponent.GeoCount + skillComponent.VentoCount;
        }

        private void TryCreateSkillContainer()
        {
            if (_skillContainer != EcsEntity.Null)
                return;

            _skillContainer = _world.NewEntity();
            _skillContainer.Get<SkillContainerComponent>();
            _skillContainer.Get<SkillComponent>();

            SetDefaultTextUIValue();
        }
        private void SetDefaultTextUIValue()
        {
            _ui.SetPyroCountText("");
            _ui.SetHydroCountText("");
            _ui.SetVentoCountText("");
            _ui.SetGeoCountText("");
        }

        private void SkillComplete(ref EcsEntity entity)
        {
            GameObject skill = new();
            EcsEntity skillEntity = _world.NewEntity();
            _skillContainer.MoveTo(skillEntity);

            ref SkillComponent component = ref skillEntity.Get<SkillComponent>();
            component.Duration = 3f;
            component.Transform = skill.transform;

            skill.AddComponent<EntityReference>().Entity = skillEntity;

            skillEntity.Del<SkillContainerComponent>();
            _skillContainer = EcsEntity.Null;
            entity.Get<SkillCooldownComponent>().Cooldown = .5f;

            SetDefaultTextUIValue();
            _skillsCircle.SetAllCircles(false);
        }

        private void SetIconUIValue(InputComponent input)
        {
            _ui.PyroPres(input.isPyro);
            _ui.HydroPres(input.isHydro);
            _ui.VentoPres(input.isVento);
            _ui.GeoPres(input.isGeo);
        }

        private void AddSkillComponent(ref InputComponent input, ref EcsEntity entity)
        {
            if (input.isPyro)
            {
                entity.Get<PyroComponent>();
                int count = ++_skillContainer.Get<SkillComponent>().PyroCount;
                _ui.SetPyroCountText(count.ToString());
                _skillsCircle.SetFireCircle(true);
            }
            else if (input.isHydro)
            {
                entity.Get<HydroComponent>();
                int count = ++_skillContainer.Get<SkillComponent>().HydroCount;
                _ui.SetHydroCountText(count.ToString());
                _skillsCircle.SetHydroCircle(true);
            }
            else if (input.isVento)
            {
                entity.Get<VentoComponent>();
                int count = ++_skillContainer.Get<SkillComponent>().VentoCount;
                _ui.SetVentoCountText(count.ToString());
                _skillsCircle.SetVentoCircle(true);
            }
            else if (input.isGeo)
            {
                entity.Get<GeoComponent>();
                int count = ++_skillContainer.Get<SkillComponent>().GeoCount;
                _ui.SetGeoCountText(count.ToString());
                _skillsCircle.SetGeoCircle(true);
            }
        }
    }
}
