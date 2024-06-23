using System;
using Code.ECS.EntityRef.Mono;

namespace Code.ECS.EntityRef
{
    [Serializable]
    public struct InitEntityReferenceComponent
    {
        public EntityReference reference;
    }
}
