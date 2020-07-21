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
            BulletStats bi = collisionInfo.Collider.GetComponent<Bullet>().bulletInfo;
            TakeDamage(bi.Damage);
            customCollision.eventBus.Publish<OnDamageTaken>(new OnDamageTaken(CurrentHealth, gameObject.GetComponent<Player>()));
        }
    }

    void TakeDamage(int ammount)
    {
        if (ammount >= CurrentHealth)
        {
            this.CurrentHealth = 0;
            customCollision.eventBus.Publish<OnPlayerDied>(new OnPlayerDied(this.gameObject));
        }
        else
        {
            this.CurrentHealth -= ammount;
        }
    }
}
