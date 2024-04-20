using Assets.Code.Scripts.View;
using System;
using UnityEngine;

[Serializable]
public struct HealthComponent
{
    public HealthUI ui;
    public float maxHealth;
    [HideInInspector] public float health;
}
