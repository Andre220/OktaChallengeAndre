using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject.SpaceFighter;
//Injetar via DI nas classes do player.
//Mudar o estado via Evento.
public class GameManager : MonoBehaviour
{
    public List<Player> Players;

    void Start()
    {
        foreach (Player p in Players)
        {
            if (p == null)
            {
                Debug.LogError("Um dos players no GameObject \"GameManager\" está nulo.");
            }
            else
            {
                p.PlayerAim.ShootEvent += TurnHandle;

                if (Players.IndexOf(p) != 0)
                {
                    p.MyTurn(false);
                }
            }
        }
    }

    void TurnHandle(Player p)
    {
        int nextPlayerIndex = Players.IndexOf(p) + 1 >= Players.Count ? 0 : Players.IndexOf(p) + 1;

        for (int i = 0; i < Players.Count; i++)
        {
            if (i == nextPlayerIndex)
            {
                Players[i].MyTurn(true);
            }
            else
            {
                Players[i].MyTurn(false);
            }
        }
    }
}
