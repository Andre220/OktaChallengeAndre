using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : CustomCollisionHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollision(GameObject go)
    {
        //Escrever o código do que deveria ser feito aqui.
        Debug.Log($"Colidi com o objeto {go.name}");

        if (go.gameObject.tag == "wall")
        {
            print("teste");
        }
    }
}
