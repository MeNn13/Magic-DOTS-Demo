using Leopotam.Ecs;
using UnityEngine;

namespace Code.ECS.Input
{
    internal class InputHandlerSystem : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
        private readonly EcsFilter<InputComponent> _filter;
        private InputHandler _inputActions;

        public void Init()
        {
            _inputActions = new InputHandler();
            _inputActions.Enable();
        }

        public void Destroy()
        {
            _inputActions.Dispose();
            _inputActions.Disable();
            _inputActions = null;
        }

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref InputComponent input = ref _filter.Get1(i);

                input.move = _inputActions.Character.Move.ReadValue<Vector2>();

                input.isAttacking = _inputActions.Character.Attack.IsPressed();
                input.isPyro = _inputActions.Character.PyroSkill.WasPressedThisFrame();
                input.isHydro = _inputActions.Character.HydroSkill.WasPressedThisFrame();
                input.isVento = _inputActions.Character.VentoSkill.WasPressedThisFrame();
                input.isGeo = _inputActions.Character.GeoSkill.WasPressedThisFrame();
            }
        }
    }
}
