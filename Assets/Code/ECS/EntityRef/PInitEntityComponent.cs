using System;
using Code.ECS.EntityRef.Mono;
using UnityEngine;
using Voody.UniLeo;

namespace Code.ECS.EntityRef
{
    [RequireComponent(typeof(EntityReference))]
    public sealed class PInitEntityComponent : MonoProvider<InitEntityReferenceComponent>
    {
    }
}