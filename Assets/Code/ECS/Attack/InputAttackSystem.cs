using Assets.Code.ECS.Input;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Code.ECS.Attack
{
    internal class InputAttackSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsFilter<InputComponent, AttackComponent> _filter;
        private ParticleSystem _flamethrower;

        public void Init()
        {
            foreach (var i in _filter)
            {
                ref AttackComponent component = ref _filter.Get2(i);

                _flamethrower = Object.Instantiate(_flamethrower, component.hand);
            }
        }

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref InputComponent input = ref _filter.Get1(i);

                if (input.isAttacking)
                    _flamethrower?.Play();
                else
                    _flamethrower?.Stop();
            }
        }
    }
}
