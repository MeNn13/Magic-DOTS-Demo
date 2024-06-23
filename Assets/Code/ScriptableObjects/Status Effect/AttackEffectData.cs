using UnityEngine;

namespace Code.ScriptableObjects.Status_Effect
{
    [CreateAssetMenu(fileName = "New Effect", menuName = "ScriptableObject/Effect/Attack")]
    public class AttackEffectData : EffectData
    {
        [SerializeField] private float damage;
        public float Damage => damage;
    }
}
