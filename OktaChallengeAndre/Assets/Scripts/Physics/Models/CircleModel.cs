using UnityEngine;

public class CircleModel : BasicPhysicsObject
{
    public float CircleRadius;
    public float CircleDiameter;

    void Update()
    {
        CircleDiameter = gameObject.transform.localScale.x;
        CircleRadius = gameObject.transform.localScale.x / 2;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, CircleRadius);
    }
}
