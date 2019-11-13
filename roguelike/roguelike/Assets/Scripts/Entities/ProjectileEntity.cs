using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEntity : MonoBehaviour
{
    [HideInInspector] public HealthEntity master;
    public bool telegraphTurn = true;
    public int stepsLast = 3;
    public int movementSpeed = 1;
    public Vector3 direction;
    public bool wallOnWay = false;
    public Animator dangerousSprite;
    //[HideInInspector]
    public List<Animator> dangerousSprites;

    public Vector3 newPos;
    public GameObject deathParticles;
    public HealthEntity damagedObject;
    public Vector3 deathPosition;
}