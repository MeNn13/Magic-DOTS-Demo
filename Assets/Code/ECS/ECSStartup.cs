﻿using Assets.Code.ECS.Attack;
using Assets.Code.ECS.EntityReference;
using Assets.Code.ECS.Health;
using Assets.Code.ECS.Input;
using Assets.Code.ECS.Moveable;
using Assets.Code.ECS.Skills.Fire;
using Assets.Code.ECS.Skills.Fire.Burning;
using Assets.Code.ECS.Skills.Pool;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Assets.Code.ECS
{
    internal class ECSStartup : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _fire;
        [SerializeField] private EffectConfig _effectConfig;

        private EcsWorld _world;
        private EcsSystems _systemsUpdate;
        private EcsSystems _systemsFixedUpdate;

        private BurnParticlePool _burningParticlePool;

        private void Awake()
        {
            _world = new();
            _systemsUpdate = new EcsSystems(_world);
            _systemsFixedUpdate = new EcsSystems(_world);

            _burningParticlePool = new(_effectConfig.BurningData.Particle, 50, "Burning Pool");

            SystemSetup();
        }

        private void SystemSetup()
        {
            _systemsUpdate?.ConvertScene();

            OneFrame();
            AddSystems();
            AddFixedSystems();
            Inject();

            _systemsUpdate?.Init();
            _systemsFixedUpdate?.Init();
        }

        private void OneFrame()
        {
            _systemsUpdate.OneFrame<InitEntityReferenceComponent>();
        }

        private void AddSystems()
        {
            _systemsUpdate.Add(new InitEntityReferenceSystem())
                .Add(new InputHandlerSystem())
                .Add(new InputAttackSystem())
                .Add(new HealthSystem())
                .Add(new HealthBurningSystem())
                .Add(new BurnSystem())
                .Add(new BurningSystem());
        }

        private void AddFixedSystems()
        {
            _systemsFixedUpdate.Add(new InputMoveableSystem());
        }

        private void Inject()
        {
            _systemsUpdate?.Inject(_fire)
            .Inject(_effectConfig)
            .Inject(_burningParticlePool);
        }

        private void Update()
        {
            _systemsUpdate?.Run();
        }

        private void FixedUpdate()
        {
            _systemsFixedUpdate?.Run();
        }

        private void OnDestroy()
        {
            _systemsUpdate.Destroy();
            _systemsUpdate = null;
            _systemsFixedUpdate.Destroy();
            _systemsFixedUpdate = null;

            _world?.Destroy();
            _world = null;
        }
    }
}
