using System;
using UnityEngine;

namespace Code.ECS.Attack
{
    [Serializable]
    public struct AttackComponent
    {
        public Transform hand;
        public bool active;
    }
}
