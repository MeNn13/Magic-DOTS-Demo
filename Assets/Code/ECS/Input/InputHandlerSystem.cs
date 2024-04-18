using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Code.ECS.Input
{
    internal class InputHandlerSystem : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
        private readonly EcsWorld _world;
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
            }
        }
    }
}
