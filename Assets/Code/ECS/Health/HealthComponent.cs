using System;
using TMPro;
using UnityEngine;
namespace Code.ECS.Health
{
    [Serializable]
    public struct HealthComponent
    {
        public HealthUI ui;
        public float maxHealth;
        public TextMeshPro damageText;
        [HideInInspector] public float health;
        [HideInInspector] public float previousHealth;
    }
}
