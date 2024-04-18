using UnityEngine;

namespace Assets.Code.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Player Data", menuName = "Scriptable/Player Data")]
    public class PlayerData : ScriptableObject
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private float _speed = 5f;

        public GameObject Prefab { get => _prefab; }
        public float Speed { get => _speed; }
    }
}