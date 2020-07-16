using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimBehaviour : MonoBehaviour
{
    public float AimRotateSpeed = 2;

    private float HorizontalAxisInput;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalAxisInput = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * HorizontalAxisInput * AimRotateSpeed * Time.deltaTime, Space.World);
    }
}
