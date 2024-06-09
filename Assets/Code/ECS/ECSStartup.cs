using Code.ECS.Status;
using Code.ECS.Attack;
using Code.ECS.EntityRef;
using Code.ECS.Health;
using Code.ECS.Input;
using Code.ECS.Movement;
using Code.ECS.Skill;
using Code.ECS.Skill.Combine;
using Code.ECS.Skill.Combine.Vape;
using Code.ECS.Skill.Cooldown;
using Code.ECS.Skill.Hydro;
using Code.ECS.Skill.Mono;
using Code.ECS.Skill.Pyro;
using Code.ECS.Status.Burning;
using Code.ECS.Status.Combine.Steam;
using Code.ECS.Status.Geo.Defending;
using Code.ECS.Status.Hydro.Soggy;
using Code.ECS.Status.Pool;
using Code.ScriptableObjects;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Code.ECS
{
    internal class EcsStartup : MonoBehaviour
    {
        [SerializeField] private SkillsConfig skillsConfig;
        [SerializeField] private EffectConfig effectConfig;
        [SerializeField] private SkillUI skillUI;
        [SerializeField] private SkillsCircle skillsCircle;
        
        private EcsWorld _world;
        private EcsSystems _systemsUpdate;
        private EcsSystems _systemsFixedUpdate;

        private PyroParticlePool _pyroParticlePool;
        private HydroParticlePool _hydroParticlePool;
        private SteamParticlePool _steamParticlePool;

        private void Awake()
        {
            _world = new EcsWorld();
            _systemsUpdate = new EcsSystems(_world);
            _systemsFixedUpdate = new EcsSystems(_world);

            _pyroParticlePool = new PyroParticlePool(effectConfig.BurningData.Particle, 50, "Pyro Pool");
            _hydroParticlePool = new HydroParticlePool(effectConfig.SoggyData.Particle, 50, "Hydro Pool");
            _steamParticlePool = new SteamParticlePool(effectConfig.SteamData.Particle, 10, "Steam Pool");

            SystemSetup();
        }

        private void SystemSetup()
        {
            _systemsUpdate?.ConvertScene();

            OneFrame();
            AddSystems();
            AddStatusSystems();
            AddSkillSystem();
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
                .Add(new HealthSystem());
        }

        private void AddSkillSystem()
        {
            _systemsUpdate.Add(new InitSkillSystem())
                .Add(new SkillSystem())
                .Add(new SkillCooldownSystem())
                .Add(new CombineSystem())
                .Add(new VapeInitSystem())
                .Add(new PyroSystem())
                .Add(new HydroSystem());
        }
        
        private void AddStatusSystems() 
        {
            _systemsUpdate.Add(new InitBurnSystem())
                .Add(new BurningSystem())
                .Add(new InitSoggySystem())
                .Add(new SoggySystem())
                .Add(new InitSteamSystem())
                .Add(new SteamSystem())
                .Add(new HealthBurningSystem())
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
            _systemsUpdate?.Inject(skillsConfig)
            .Inject(effectConfig)
            .Inject(_pyroParticlePool)
            .Inject(_hydroParticlePool)
            .Inject(_steamParticlePool)
            .Inject(skillUI)
            .Inject(skillsCircle);
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
