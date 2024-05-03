using System;
using UnityEngine;

namespace Code.ECS.Input
{
    [Serializable]
    public struct InputComponent
    {
        public Vector2 move;
        public bool isAttacking;
        public bool isPyro;
        public bool isHydro;
        public bool isVento;
        public bool isGeo;
    }
}
