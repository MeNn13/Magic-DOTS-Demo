using UnityEngine;
using System;

namespace Assets.Code.ECS.Moveable
{
    [Serializable]
    public struct MoveableComponent
    {
        public Transform transform;
        public float speed;
        public float rotateSpeed;
    }
}
