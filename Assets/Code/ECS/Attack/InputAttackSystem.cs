using Code.ECS.EntityRef.Mono;
using Code.ECS.Input;
using Code.ECS.Skill;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.ECS.Attack
{
    internal class InputAttackSystem : IEcsRunSystem
    {
        private readonly EcsFilter<InputComponent, AttackComponent> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref InputComponent input = ref _filter.Get1(i);
                ref AttackComponent attack = ref _filter.Get2(i);

                if (CheckHasSkill(ref attack))
                    return;

                if (!input.isAttacking)
                    SkillDestroy(attack);
                else
                    attack.active = true;
            }
        }
        private static void SkillDestroy(AttackComponent attack)
        {
            attack.active = false;
            GameObject skill = attack.hand.GetChild(0).gameObject;
            EcsEntity entity =  skill.GetComponent<EntityReference>().Entity;
            Object.Destroy(entity.Get<SkillComponent>().Transform.gameObject);
            entity.Destroy();
            Object.Destroy(skill);
        }
        
        private static bool CheckHasSkill(ref AttackComponent attack)
        {
            if (attack.hand.childCount == 0)
            {
                attack.active = false;
                return true;
            }

            return false;
        }
    }
}
