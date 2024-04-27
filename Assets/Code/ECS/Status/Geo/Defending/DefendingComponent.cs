using UnityEngine;

public struct DefendingComponent
{
    public Transform objTransform;
    public ParticleSystem particle;
    public Material material;
    public Material[] previousMaterials;
    public float duration;
    public float defensePoints;
}