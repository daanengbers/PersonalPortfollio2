using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class testing : MonoBehaviour
{
    [SerializeField] private Player_AimWeapon player_AimWeapon;


    private void Start()
    {
        player_AimWeapon.OnShoot += player_AimWeapon_OnShoot;
    }

    private void player_AimWeapon_OnShoot(object sender, Player_AimWeapon.OnShootEventArgs e)
    {
        UtilsClass.ShakeCamera(1f, .2f);
        Debug.Log("Hello");
        //WeaponTracer.Create(e.gunEndPointPosition, e.shootPosition);
        //Shoot_Flash.AddFlash(e.gunEndPointPosition);
    }
}