using UnityEngine;

[CreateAssetMenu(fileName = "New Effect", menuName = "ScriptableObject/Effect/Attack")]
public class AttackEffectData : EffectData
{
    [SerializeField] private int _damage;
    public int Damage { get => _damage; }
}
