using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject.SpaceFighter;
//Injetar via DI nas classes do player.
//Mudar o estado via Evento.
public class GameManager : MonoBehaviour
{
    public int currentPlayerIndex;
    public List<Player> Players;

    void Start()
    {
        currentPlayerIndex = 0;

        foreach (Player p in Players)
        {
            if (p == null)
            {
                Debug.LogError("Um dos players no GameObject \"GameManager\" está nulo.");
            }
            else
            {
                if (Players.IndexOf(p) != currentPlayerIndex)
                {
                    p.MyTurn(false);
                }
            }
        }
    }

    public void EnableNextPlayer()
    {
        int nextPlayerIndex = currentPlayerIndex + 1 >= Players.Count ? 0 : currentPlayerIndex + 1;

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

        currentPlayerIndex = nextPlayerIndex;
    }

    public void DisableCurrentPlayerInput() // Usado quando um player acabou de fazer um disparo e a bala ainda nao terminou seu percurso.
    {
        Players[currentPlayerIndex].MyTurn(false);
    }
}
