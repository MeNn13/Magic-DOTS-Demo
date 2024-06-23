using UnityEngine;

[CreateAssetMenu(fileName = "New Effect", menuName = "ScriptableObject/Effect/Default")]
public class EffectData : ScriptableObject
{
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private Material[] materials;
    [SerializeField] private float duration;

    public ParticleSystem Particle { get => particle; }
    public Material[] Materials { get => materials; }
    public float Duration { get => duration; }
}
