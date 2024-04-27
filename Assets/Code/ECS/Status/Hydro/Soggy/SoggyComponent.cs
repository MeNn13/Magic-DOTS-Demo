using UnityEngine;

public struct SoggyComponent 
{
    public Transform objTransform;
    public ParticleSystem particle;
    public Material[] materials;
    public Material[] previousMaterials;
    public float duration;
}