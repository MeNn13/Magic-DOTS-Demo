using UnityEngine;

[CreateAssetMenu(fileName = "New Effect", menuName = "ScriptableObject/Effect")]
public class EffectData : ScriptableObject
{
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private float _duration;
    [SerializeField] private int _multiplyDamage;

    public ParticleSystem Particle { get => _particle; }
    public float Duration { get => _duration; }
    public int MultiplyDamage { get => _multiplyDamage; }
}
