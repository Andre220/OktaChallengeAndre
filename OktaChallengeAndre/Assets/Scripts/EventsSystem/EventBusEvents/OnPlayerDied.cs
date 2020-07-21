using GameEventBus.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlayerDied : EventBase
{
    public GameObject DeadPlayer;

    public OnPlayerDied(GameObject playerDied)
    {
        DeadPlayer = playerDied;
    }
}
