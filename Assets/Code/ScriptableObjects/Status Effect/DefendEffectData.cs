using UnityEngine;
namespace Code.ScriptableObjects.Status_Effect
{
    [CreateAssetMenu(fileName = "New Effect", menuName = "ScriptableObject/Effect/Defend")]
    public class DefendEffectData : EffectData
    {
        [SerializeField] private float defensePoints;

        public float DefensePoints => defensePoints;
    }
}
