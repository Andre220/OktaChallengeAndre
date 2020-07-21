using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

/// <summary>
/// Responsavel por "observar" eventos de colisao e, caso um desses eventos
/// inclua um objeto do tipo bala e o objeto em que este MonoBehaviour esta anexado,
/// invocar o evento OnDamageTaken.
/// 
/// Tambem reduz a quantidade de vida do Player e invoca o evento OnPlayerDied quando a vida do player e menor ou igual a zero.
/// </summary>
 
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
            Bullet bi = collisionInfo.Collider.GetComponent<Bullet>();

            if ((this.gameObject != bi.Shooter) || bi.bulletInfo.CanDamageShooter) // O dano só é aplicado caso este objeto não seja o atirador OU caso esta bala possa causar dano ao próprio atirador, como é com a KillerQueen
            {
                TakeDamage(bi.bulletInfo.Damage);
                customCollision.eventBus.Publish<OnDamageTaken>(new OnDamageTaken(CurrentHealth, gameObject.GetComponent<Player>()));
            }
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
