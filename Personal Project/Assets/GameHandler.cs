using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private CameraFollow cameraFollow;
    [SerializeField] private Player_AimWeapon player_AimWeapon;
    public Transform playerTransform;

    private void Start()
    {
        cameraFollow.Setup(() => playerTransform.position );
        //player_AimWeapon.OnShoot += player_AimWeapon_OnShoot;
    }
}
