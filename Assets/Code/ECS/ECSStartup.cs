using Assets.Code.ECS.Gravity;
using Assets.Code.ECS.Input;
using Assets.Code.ECS.Moveable;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Assets.Code.ECS
{
    internal class ECSStartup : MonoBehaviour
    {
        EcsWorld _world;
        EcsSystems _systems;

        private void Awake()
        {
            _world = new();
            _systems = new EcsSystems(_world);

            _systems?.ConvertScene();
            AddSystems();
            _systems?.Init();
        }

        private void AddSystems()
        {
            _systems.Add(new InputHandlerSystem())
                .Add(new InputMoveableSystem())
                .Add(new GravitySystem());
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
