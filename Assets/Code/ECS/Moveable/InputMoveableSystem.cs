using Assets.Code.ECS.Input;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Code.ECS.Moveable
{
    public class InputMoveableSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsFilter<InputComponent, MoveableComponent> _filter;
        private Rigidbody _rb;

        public void Init()
        {
            foreach (var i in _filter)
            {
                ref MoveableComponent component = ref _filter.Get2(i);

                _rb = component.transform.GetComponent<Rigidbody>();
            }
        }

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref InputComponent input = ref _filter.Get1(i);
                ref MoveableComponent move = ref _filter.Get2(i);

                Vector3 moveDirection = new(input.move.x, 0, input.move.y);

                Movement(moveDirection, move.transform, move.speed, move.rotateSpeed);
            }
        }
        private void Movement(Vector3 direction, Transform transform, float speed, float rotateSpeed)
        {
            if (direction.magnitude >= .1f)
            {
                Quaternion rotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotateSpeed * Time.fixedDeltaTime);
            }

            Vector3 velocity = direction * speed * Time.fixedDeltaTime;
            velocity.y = _rb.velocity.y;
            _rb.velocity = velocity;
        }
    }
}
