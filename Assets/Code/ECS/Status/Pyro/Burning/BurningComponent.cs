using UnityEngine;

namespace Code.ECS.Status.Burning
{
    public struct BurningComponent
    {
        public Transform ObjTransform;
        public ParticleSystem Particle;
        public Material[] Materials;
        public Material[] PreviousMaterials;
        public float Duration;
        public float Damage;
    }
}
