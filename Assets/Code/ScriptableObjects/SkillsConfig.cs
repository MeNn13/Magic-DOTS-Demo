using UnityEngine;

namespace Code.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Skill", menuName = "ScriptableObject/Skill")]
    public class SkillsConfig : ScriptableObject
    {
        [SerializeField] private ParticleSystem pyroSkill;
        [SerializeField] private ParticleSystem hydroSkill;
        [SerializeField] private ParticleSystem ventoSkill;
        [SerializeField] private ParticleSystem geoSkill;

        public ParticleSystem PyroSkill => pyroSkill;
        public ParticleSystem HydroSkill => hydroSkill;
        public ParticleSystem VentoSkill => ventoSkill;
        public ParticleSystem GeoSkill => geoSkill;
    }
}
