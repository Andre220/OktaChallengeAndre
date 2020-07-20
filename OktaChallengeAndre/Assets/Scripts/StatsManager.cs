using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StatsManager : MonoBehaviour
{
    public PlayerCombatInfo PlayerHealth;

    public GameEvent OnHitted;

    [Inject]
    public ICustomCollision customCollision;

    void OnEnable()
    {
        customCollision.OnCollisionEvent += OnCollision;
    }

    private void OnDestroy()
    {
        customCollision.OnCollisionEvent -= OnCollision;
    }

    void OnCollision(BasicPhysicsObject bpo) //Forma atual de fazer um "callback" para colisões
    {
        print(bpo.name);

        if (bpo.gameObject.tag == "Bullet")
        {
            TakeDamage(bpo.gameObject.GetComponent<Bullet>().bulletInfo.Damage);
            OnHitted.Raise();
        }
    }

    void TakeDamage(int ammount)
    {
        if (ammount > PlayerHealth.PlayerHP)
        {
            PlayerHealth.PlayerHP = 0;
        }
        else
        {
            PlayerHealth.PlayerHP -= ammount;
        }
    }
}
