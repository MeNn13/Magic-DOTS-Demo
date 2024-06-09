using System;
using UnityEngine;
namespace Code.ECS.Health
{
    [Serializable]
    public struct HealthComponent
    {
        public HealthUI ui;
        public float maxHealth;
        [HideInInspector] public float health;
    }
}
