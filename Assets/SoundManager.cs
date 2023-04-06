using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioClip walk;
    public AudioClip attack;

    public AudioClip death;

    public AudioClip florence;

    public AudioClip life;

    public AudioSource manager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void WalkSound()
    {
        manager.PlayOneShot(walk);
    }

    public void HitSound()
    {
        manager.PlayOneShot(attack);
    }

    public void DeathSound()
    {
        manager.PlayOneShot(death);
    }

    public void FlorSound()
    {
        manager.PlayOneShot(florence);
    }

    public void LifeSound()
    {
        manager.PlayOneShot(life);
    }
}
