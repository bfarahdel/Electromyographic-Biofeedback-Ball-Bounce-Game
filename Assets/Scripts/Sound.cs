using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // want class to appear in inspector
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    [Range(0f, 1f)]
    public float pitch;

    [HideInInspector]
    public AudioSource source; //need to make this public but don't want it to appear in inspector
}

