using Assets.Code.ECS.Attack;
using Assets.Code.ECS.EntityRef;
using Assets.Code.ECS.Health;
using Assets.Code.ECS.Status.Combine.Steam;
using Assets.Code.ECS.Status.Geo.Defending;
using Assets.Code.ECS.Status.Hydro.Soggy;
using Assets.Code.ECS.Status.Pool;
using Assets.Code.ECS.Status.Pyro;
using Assets.Code.ECS.Status.Pyro.Burning;
using Code.ECS.Input;
using Code.ECS.Moveable;
using Code.ECS.Skill;
using Code.ECS.Skill.Cooldown;
using Code.ECS.Skill.Mono;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Code.ECS
{
    internal class EcsStartup : MonoBehaviour
    {
        [SerializeField] private ParticleSystem fire;
        [SerializeField] private EffectConfig effectConfig;
        [SerializeField] private SkillUI skillUI;
        
        private EcsWorld _world;
        private EcsSystems _systemsUpdate;
        private EcsSystems _systemsFixedUpdate;

        private PyroParticlePool _pyroParticlePool;
        private HydroParticlePool _hydroParticlePool;
        private SteamParticlePool _steamParticlePool;

        private void Awake()
        {
            _world = new EcsWorld();
            _systemsUpdate = new EcsSystems(world: _world);
            _systemsFixedUpdate = new EcsSystems(world: _world);

            _pyroParticlePool = new PyroParticlePool(prefab: effectConfig.BurningData.Particle, prewarmObjectCount: 50, poolName: "Pyro Pool");
            _hydroParticlePool = new HydroParticlePool(prefab: effectConfig.SoggyData.Particle, prewarmObjectCount: 50, poolName: "Hydro Pool");
            _steamParticlePool = new SteamParticlePool(prefab: effectConfig.SteamData.Particle, prewarmObjectCount: 10, poolName: "Steam Pool");

            SystemSetup();
        }

        private void SystemSetup()
        {
            _systemsUpdate?.ConvertScene();

            OneFrame();
            AddSystems();
            AddStatusSystems();
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
                .Add(new InitSkillSystem())
                .Add(new SkillSystem())
                .Add(new SkillCooldownSystem());
        }

        private void AddStatusSystems() 
        {
            _systemsUpdate.Add(new InitBurnSystem())
                .Add(new BurningSystem())
                .Add(new InitSoggySystem())
                .Add(new SoggySystem())
                .Add(new InitSteamSystem())
                .Add(new SteamSystem())
                .Add(new InitDefendingSystem())
                .Add(new HealthDefenseSystem())
                .Add(new DefendBurningSystem());
        }

        private void AddFixedSystems()
        {
            _systemsFixedUpdate.Add(new InputMovablesSystem());
        }

        private void Inject()
        {
            _systemsUpdate?.Inject(obj: fire)
            .Inject(obj: effectConfig)
            .Inject(obj: _pyroParticlePool)
            .Inject(obj: _hydroParticlePool)
            .Inject(obj: _steamParticlePool)
            .Inject(obj: skillUI);
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
