using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCombatInfo", menuName = "scriptables/PlayerCombatInfo", order = 0)]
public class PlayerCombatInfo : ScriptableObject
{
    public int StartPlayerHP;

    [HideInInspector]
    public int PlayerHP;

    private void Awake()
    {
        PlayerHP = StartPlayerHP;
    }
}
