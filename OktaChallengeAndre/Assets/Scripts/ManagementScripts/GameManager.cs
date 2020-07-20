using GameEventBus.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;
//Injetar via DI nas classes do player.
//Mudar o estado via Evento.
public class GameManager : MonoBehaviour
{
    public int currentPlayerIndex = 0;

    public List<Player> Players;

    [Inject]
    public IEventBus eventBus;

    void OnEnable()
    {
        eventBus.Subscribe<OnBulletShooted>(DisablePlayersInput); // Quando um disparo é efetuado, todos os jogadores ficam imóveis até o momento que a bala é destruida.
        eventBus.Subscribe<OnBulletDestroyed>(EnableNextPlayer); 
    }

    void OnDisable()
    {
        eventBus.Unsubscribe<OnBulletShooted>(DisablePlayersInput);
        eventBus.Unsubscribe<OnBulletDestroyed>(EnableNextPlayer); 
    }

    void Start()
    {
        Players[currentPlayerIndex].MyTurn(true);

        for (int i = 1; i <= Players.Count - 1; i++)
        {
            Players[i].MyTurn(false);
        }
    }

    public void EnableNextPlayer(OnBulletDestroyed o)
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

    public void DisablePlayersInput(OnBulletShooted currentPlayer)
    {
        foreach (Player p in Players)
        {
            p.MyTurn(false);
        }
    }
}
