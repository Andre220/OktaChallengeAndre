using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovement : MonoBehaviour
{
    public float MovementSpeed = 2;

    private float VerticalAxisInput;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        VerticalAxisInput = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.up * VerticalAxisInput * MovementSpeed * Time.deltaTime, Space.World);
    }
}
