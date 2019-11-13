using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicGeneratorSystem : MonoBehaviour
{
    GameManager gm;
    AudioSource au;

    public AudioClip kickDrum;
    public AudioClip snareDrum;


    public void Init()
    {
        gm = GameManager.instance;
        au = GetComponent<AudioSource>();
        au.clip = kickDrum;
    }

    public void Step()
    {
        if (au.clip == kickDrum) au.clip = snareDrum;
        else if (au.clip == snareDrum) au.clip = kickDrum;

        au.Play();
    }
}
