using Assets.Code.ECS.EntityReference;
using Assets.Code.ECS.Gravity;
using Assets.Code.ECS.Input;
using Assets.Code.ECS.Moveable;
using Assets.Code.ECS.Skills.Fire;
using Assets.Code.ECS.Skills.Fire.Burning;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Assets.Code.ECS
{
    internal class ECSStartup : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _fire;

        EcsWorld _world;
        EcsSystems _systems;

        private void Awake()
        {
            _world = new();
            _systems = new EcsSystems(_world);

            _systems?.ConvertScene();
            OneFrame();
            AddSystems();
            Inject();
            _systems?.Init();
        }

        private void OneFrame()
        {
            _systems.OneFrame<InitEntityReferenceComponent>();
        }

        private void AddSystems()
        {
            _systems.Add(new InitEntityReferenceSystem())
                .Add(new InputHandlerSystem())
                .Add(new InputMoveableSystem())
                .Add(new GravitySystem())
                .Add(new BurnSystem())
                .Add(new BurningSystem());
        }

        private void Inject()
        {
            _systems?.Inject(_fire);
        }

        private void Update()
        {
            _systems?.Run();
        }

        private void OnDestroy()
        {
            _systems?.Destroy();
            _systems = null;
            _world?.Destroy();
            _world = null;
        }
    }
}
