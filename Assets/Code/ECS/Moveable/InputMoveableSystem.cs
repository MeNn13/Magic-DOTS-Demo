using Assets.Code.ECS.Input;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Code.ECS.Moveable
{
    public class InputMoveableSystem : IEcsRunSystem
    {
        private readonly EcsFilter<InputComponent, MoveableComponent> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref InputComponent input = ref _filter.Get1(i);
                ref MoveableComponent move = ref _filter.Get2(i);

                Move(input.move, move.transform, move.speed, move.rotateSpeed);
            }
        }
        private void Move(Vector2 inputMove, Transform transform, float speed, float rotateSpeed)
        {
            Vector3 moveDirection = new(inputMove.x, 0, inputMove.y);

            if (moveDirection.magnitude >= .1f)
            {
                Quaternion rotation = Quaternion.LookRotation(moveDirection);
                rotation.x = 0;
                rotation.z = 0;
                transform.rotation = (Quaternion.Lerp(transform.rotation, rotation, rotateSpeed * Time.fixedDeltaTime));
            }

            transform.position += moveDirection * speed * Time.deltaTime;
        }
    }
}
