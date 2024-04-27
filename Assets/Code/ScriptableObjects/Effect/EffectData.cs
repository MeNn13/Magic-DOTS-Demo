using UnityEngine;

[CreateAssetMenu(fileName = "New Effect", menuName = "ScriptableObject/Effect/Default")]
public class EffectData : ScriptableObject
{
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private Material _material;
    [SerializeField] private float _duration;

    public ParticleSystem Particle { get => _particle; }
    public Material Material { get => _material; }
    public float Duration { get => _duration; }
}
