using UnityEngine;
using System;

public class AudioManagerSystem : MonoBehaviour
{
    public Sounds[] sounds;
    public void PlaySound(string name)
    {

        Sounds s = Array.Find(sounds, sound => sound.name == name);
        Debug.Log(s.name);
        s.source.Play();
    }
    void Awake()
    {
        foreach (Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }
}
