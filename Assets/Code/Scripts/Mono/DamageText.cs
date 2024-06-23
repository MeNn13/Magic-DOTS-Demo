using System;
using Code.ECS.DamageTextUI;
using Code.ECS.EntityRef.Mono;
using Leopotam.Ecs;
using TMPro;
using UnityEngine;

namespace Code.Scripts.Mono
{
    [RequireComponent(typeof(EntityReference)
        ,typeof(TextMeshPro))]
    public class DamageText : MonoBehaviour
    {
        private EntityReference _entityRef;
        private TextMeshPro _text;

        private void Start()
        {
            _entityRef = GetComponent<EntityReference>();
            _text = GetComponent<TextMeshPro>();
        }

        public void AnimationEnd()
        {
            _entityRef.Entity.Get<DamageTextEndTriggerComponent>().Text = _text;
        }
    }
}
