using UnityEngine;

public struct BurningComponent
{
    public Transform objTransform;
    public ParticleSystem particle;
    public Material[] materials;
    public Material[] previousMaterials;
    public float duration;
    public float damage;
}
