using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)] public float volume;
    [Range(0.1f, 3f)] public float pitch;

    [Range(0f, 0.5f)] public float randomVolume = 0.1f, randomPitch = 0.1f;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
