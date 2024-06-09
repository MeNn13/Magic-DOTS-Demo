using UnityEngine;
namespace Code.ECS.Status.Geo.Defending
{
    public struct DefendingComponent
    {
        public Transform ObjTransform;
        public ParticleSystem Particle;
        public Material[] Materials;
        public Material[] PreviousMaterials;
        public float Duration;
        public float DefensePoints;
    }
}