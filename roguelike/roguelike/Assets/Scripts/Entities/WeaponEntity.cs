using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEntity : MonoBehaviour
{
    public enum AimType {Cross, Circle};
    public AimType aimType = AimType.Cross;

    public ProjectileEntity projectile;
}