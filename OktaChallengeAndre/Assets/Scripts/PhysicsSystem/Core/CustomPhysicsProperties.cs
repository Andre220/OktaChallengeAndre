using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CustomPhysicsProperties", menuName = "scriptables/physics/CustomPhysicsProperties", order = 0)]
public class CustomPhysicsProperties : ScriptableObject
{
    public bool isStatic;
    public bool canBounce;
    public bool stopOnCollide;
}
