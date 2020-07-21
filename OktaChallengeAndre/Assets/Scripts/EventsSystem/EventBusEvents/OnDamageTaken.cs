using GameEventBus.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDamageTaken : EventBase
{
    public int CurrentHealth;
    public Player PlayerDamaged;

    public OnDamageTaken(int currentHealth, Player playerDamaged)
    {
        CurrentHealth = currentHealth;
        PlayerDamaged = playerDamaged;
    }
}
