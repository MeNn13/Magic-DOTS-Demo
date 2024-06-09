using UnityEngine;
namespace Code.ECS.Status.Hydro.Soggy
{
    public struct SoggyComponent 
    {
        public Transform ObjTransform;
        public ParticleSystem Particle;
        public Material[] Materials;
        public Material[] PreviousMaterials;
        public float Duration;
    }
}