using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEntity : MonoBehaviour
{
    public int health = 3;
    [HideInInspector] public int healthMax = 3;

    [HideInInspector] public NpcEntity npc;
    [HideInInspector] public Animator anim;
}