using GameEventBus.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    public int currentPlayerIndex = 0;

    public List<Player> Players;

    [Inject]
    public IEventBus eventBus;

    void OnEnable()
    {
        eventBus.Subscribe<OnBulletShooted>(DisablePlayersInput);
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

    /// <summary>
    /// Esta função não utiliza o retorno do evento pois eventualmente o colisor e o "colidido" vem trocados.
    /// Algo a se aprimorar em uma futura iteração
    /// <summary>
    public void EnableNextPlayer(OnBulletDestroyed obj)
    {
        //Caso o indice do proximo jogador seja maior que a quantidade de jogadores, o proximo jogador é o primeiro da lista (0)
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

    /// <summary>
    /// Esta função é chamada quando a bala é disparada.
    /// A lógica aqui é impedir o input de TODOS os jogadores enquanto o projétil existir em cena, impedindo 
    /// que o adversário possa fugir do projétil caso ainda não seja seu turno.
    /// <summary>
    public void DisablePlayersInput(OnBulletShooted currentPlayer)
    {
        foreach (Player p in Players)
        {
            p.MyTurn(false);
        }
    }
}
