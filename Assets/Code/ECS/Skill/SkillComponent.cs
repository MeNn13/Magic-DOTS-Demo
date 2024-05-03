using UnityEngine;
namespace Code.ECS.Skill
{
    public struct SkillComponent
    {
        public Transform Transform;
        public float Duration;
        public byte PyroCount;
        public byte HydroCount;
        public byte VentoCount;
        public byte GeoCount;
    }
}