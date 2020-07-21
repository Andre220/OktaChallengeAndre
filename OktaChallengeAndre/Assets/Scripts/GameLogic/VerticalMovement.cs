using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovement : MonoBehaviour
{
    public float MovementSpeed = 2;

    private float VerticalAxisInput;

    public BasicPhysicsObject bpo;

    void Start()
    {
        try
        {
            bpo = GetComponent<BasicPhysicsObject>();
        }
        catch
        {
            Debug.LogError("BasicPhysicsObject não encontrado. Por favor, adicionar CustomCircleCollider ou CustomSquareCollider");
            DestroyImmediate(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        VerticalAxisInput = Input.GetAxisRaw("Vertical");

        bpo.Velocity = new Vector3(0, VerticalAxisInput * MovementSpeed, 0);
    }

    void FixedUpdate()
    {
    }
}
