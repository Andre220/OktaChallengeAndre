using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BasicPhysicsObject))]
public class CustomCollisionHandler : MonoBehaviour
{
    public BasicPhysicsObject BPO;

    void OnEnable()
    {
        BPO = gameObject.GetComponent<BasicPhysicsObject>();
        BPO.OnCollision += OnCollision;
    }

    void OnDisable()
    {
        BPO.OnCollision -= OnCollision;
    }

    public void OnCollision(GameObject go)
    {
        //Escrever o código do que deveria ser feito aqui.
        print(go.gameObject.name);
    }
}
