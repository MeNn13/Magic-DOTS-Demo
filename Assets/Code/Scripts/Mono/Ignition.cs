using Assets.Code.ECS.EntityRef;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Code.Scripts.Mono
{
    public class Ignition : MonoBehaviour
    {
        private const string _tag = "Burnable";
        private const string _playerTag = "Player";

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag(_tag))
            {
                EcsEntity entity = other.GetComponent<EntityReference>().Entity;
                entity.Get<BurnTriggerComponent>().collider = other;
            }
            else if (other.CompareTag(_playerTag))
            {
                EcsEntity entity = other.GetComponent<EntityReference>().Entity;
                entity.Get<BurnTriggerComponent>().collider = other;
            }
        }
    }
}