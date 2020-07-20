using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StatsManager : MonoBehaviour
{
    public int CurrentHealth;

    [Inject]
    public ICustomCollision customCollision;

    void OnEnable()
    {
        customCollision.eventBus.Subscribe<OnPhysicsObjectCollide>(OnCollision);
    }

    private void OnDestroy()
    {
        customCollision.eventBus.Unsubscribe<OnPhysicsObjectCollide>(OnCollision);
    }

    void OnCollision(OnPhysicsObjectCollide collisionInfo) //Forma atual de fazer um "callback" para colisões
    {
        if (collisionInfo.Collider.tag == "Bullet" && collisionInfo.Collided.gameObject == this.gameObject)
        {
            print(collisionInfo.Collider.name);

            BulletInfo bi = collisionInfo.Collider.GetComponent<Bullet>().bulletInfo;
            TakeDamage(bi.Damage);
            //print(CurrentHealth);
            customCollision.eventBus.Publish<OnDamageTaken>(new OnDamageTaken(CurrentHealth, gameObject.GetComponent<Player>()));
        }
    }

    void TakeDamage(int ammount)
    {
        if (ammount > CurrentHealth)
        {
            this.CurrentHealth = 0;
        }
        else
        {
            this.CurrentHealth -= ammount;
        }
    }
}
