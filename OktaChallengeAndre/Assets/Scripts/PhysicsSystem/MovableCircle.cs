using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleModel))]
public class MovableCircle : MonoBehaviour
{
    //private Vector3 Velocity;

    private BasicPhysicsObject physicsObject;
    // Start is called before the first frame update
    void Start()
    {
        physicsObject = GetComponent<BasicPhysicsObject>();
        //.GetComponent<CircleCollision>().OnSquareCollision += OnCollision;
        //gameObject.GetComponent<CircleCollision>().OnCircleCollision += OnCollision;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += physicsObject.Velocity * Time.deltaTime;
    }

    private void OnCollision()
    {
        //Velocity = Vector3.zero;
    }
}
