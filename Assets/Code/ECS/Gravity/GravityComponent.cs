using System;
using UnityEngine;

[Serializable]
public struct GravityComponent
{
    public Transform transformObject;
    public Transform groundSpherePosition;
    public LayerMask groundMask;
    public Vector3 velocity;
    public float groundSphereRadius;
    public float gravity;
    public bool isGrounded;
}
