using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcEntity : MonoBehaviour
{
    [HideInInspector] public HealthEntity health;

    public bool canMove = true;
    public int viewRange = 5; // in tiles

    public int shotCooldownMax = 5;
    public int shotCooldownCurrent = 5;
    public WeaponEntity weaponEntity;
    public int moveCooldown = 0;
    public Vector3 savedPosition;

    public ProjectileEntity projectileToFire;

    public HealthEntity attackTarget;
}