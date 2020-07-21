
using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioMixerGroup audioMixerGroup;

    public Sound[] sounds;

    [HideInInspector]
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);


        foreach (Sound s in sounds)
        {
            // add an AudioSource component and transfer attributes of the Sound to that source
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.playOnAwake = false;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = audioMixerGroup;
        }
    }

    public void Start()
    {
        PlaySound("Music");
        StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "MasterVolume", 1.0f, 1.0f));
    }

    public void PlaySound(string name)
    {
        //Debug.Log("AudioManager::PlaySound(string)");
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Play();
    }
}
