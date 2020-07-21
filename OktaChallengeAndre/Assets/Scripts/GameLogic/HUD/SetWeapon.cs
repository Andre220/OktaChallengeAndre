using UnityEngine;

/// <summary>
/// 
/// Este monobehaviour e utilizado na HUD
/// para mudar a bala escolhida pelo player
/// quando ele interagir com a HUD.
/// 
/// </summary>

public class SetWeapon : MonoBehaviour
{
    public AimAndShoot PlayerAimAndShoot;
    public void SetPlayerBullet(BulletStats bulletStats)
    {
        PlayerAimAndShoot.ChoosedBullet = bulletStats;
    }
}
