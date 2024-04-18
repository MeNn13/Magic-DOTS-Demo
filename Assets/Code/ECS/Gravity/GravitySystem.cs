using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Code.ECS.Gravity
{
    internal class GravitySystem : IEcsRunSystem
    {
        private readonly EcsFilter<GravityComponent> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref GravityComponent component = ref _filter.Get1(i);

                component.isGrounded = Physics.CheckSphere(
                    component.groundSpherePosition.position,
                    component.groundSphereRadius,
                    component.groundMask);

                if (component.isGrounded || component.velocity.y < 0)
                    component.velocity.y = 0;

                component.velocity.y += component.gravity * Time.deltaTime;

                if (!component.isGrounded)
                    component.transformObject.position += component.velocity;
            }
        }
    }
}
