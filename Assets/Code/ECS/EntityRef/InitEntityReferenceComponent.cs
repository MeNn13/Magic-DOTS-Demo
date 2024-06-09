using System;

namespace Code.ECS.EntityRef
{
    [Serializable]
    public struct InitEntityReferenceComponent
    {
        public Mono.EntityReference reference;
    }
}
