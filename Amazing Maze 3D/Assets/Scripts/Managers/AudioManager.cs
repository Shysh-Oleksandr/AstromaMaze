using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : GenericSingletonClass<AudioManager>
{
    public Sound[] sounds;

    private new void Awake()
    {
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume * (1 + UnityEngine.Random.Range(-s.randomVolume / 2f, s.randomVolume / 2f));
            s.source.pitch = s.pitch * (1 + UnityEngine.Random.Range(-s.randomPitch / 2f, s.randomPitch / 2f));
            s.source.loop = s.loop;
        }
    }


    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Play();
        print(name + " played.");
    }
}
