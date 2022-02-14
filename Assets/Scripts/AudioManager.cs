using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sound item in sounds) // iterate through sound array (inspector window)
        {
            // adds audio source component for each sound in sound array
            item.source = gameObject.AddComponent<AudioSource>();  
            item.source.clip = item.clip;
            item.source.volume = item.volume;
            item.source.pitch = item.pitch;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name); // looks for sound in inspector window
        s.source.PlayOneShot(s.clip);
    }
}
