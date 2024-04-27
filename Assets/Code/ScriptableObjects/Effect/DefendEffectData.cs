using UnityEngine;

[CreateAssetMenu(fileName = "New Effect", menuName = "ScriptableObject/Effect/Defend")]
public class DefendEffectData : EffectData
{
    [SerializeField] private float _defensePoints;

    public float DefensePoints { get => _defensePoints; }
}
