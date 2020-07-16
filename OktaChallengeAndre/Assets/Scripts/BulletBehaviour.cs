using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public int maxBounces = 3;
    public int Damage = 1;

    private int bounceCount;

    void Start()
    {
        gameObject.tag = "Bullet";
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            if (bounceCount >= maxBounces)
                Destroy(gameObject);
            else
                bounceCount += 1;
        }
        else if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }


}
