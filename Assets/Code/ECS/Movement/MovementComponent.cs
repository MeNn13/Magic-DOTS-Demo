using System;
using UnityEngine;

namespace Code.ECS.Movement
{
    [Serializable]
    public struct MovementComponent
    {
        public Transform transform;
        public float speed;
        public float rotateSpeed;
    }
}
