using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; } 
    private AudioSource source;

    private void Awake()
    {
        instance = this;
        source = GetComponent<AudioSource>();

        //keep this object even when we go to new scene
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //destroy duplicate gameobject
        else if (instance != null && instance != this)
        Destroy(gameObject);
       
    }

    public void PlaySound(AudioClip _sound)
    {
        source.PlayOneShot(_sound);
    }
}
