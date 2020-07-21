using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Ao pressionar o botar para jogar apos selecionar a arma de cada jogador,
/// e necessario confirmar se ambos os jogadores escolheram armas
/// </summary>

public class ChooseWeaponWindow : MonoBehaviour
{
    public AimAndShoot[] PlayersAimAndShoot;

    public GameObject ChooseWeaponHUD;

    public void OnContinueButton(Text errorText)
    {
        bool canDisable = !PlayersAimAndShoot.Any(current => current.ChoosedBullet == null);

        if (canDisable == true)
        {
            ChooseWeaponHUD.SetActive(false);
        }
        else
        {
            errorText.text = "Um dos jogadores não escolheu uma arma.";
        }
    }
}
