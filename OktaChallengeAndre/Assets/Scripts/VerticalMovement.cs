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
            bpo.Velocity = new Vector3(0, MovementSpeed, 0);
        }
        catch
        {
            Debug.LogError("BasicPhysicsObject não encontrado. Por favor, adicionar CircleModel ou SquareModel");
            DestroyImmediate(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        VerticalAxisInput = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        bpo.Velocity = new Vector3(0, VerticalAxisInput * MovementSpeed,0);
        //transform.Translate(Vector3.up * VerticalAxisInput * MovementSpeed * Time.deltaTime, Space.World);
    }
}
