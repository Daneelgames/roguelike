using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicGeneratorSystem : MonoBehaviour
{
    GameManager gm;
    public AudioSource auHat;
    public AudioSource auSnare;
    public AudioSource auKick;

    float hiHatDynamicCooldown = 0.5f;
    int barState = 0;

    public void Init()
    {
        gm = GameManager.instance;
    }

    public void Step()
    {
        switch (barState)
        {
            case 0:
                //auHat.Play();
                auKick.Play();
                barState++;
                break;
            case 1:
                auHat.Play();
                barState++;
                break;
            case 2:
                //auHat.Play();
                auSnare.Play();
                barState++;
                break;
            case 3:
                auHat.Play();
                barState = 0;
                break;
        }
    }
}
