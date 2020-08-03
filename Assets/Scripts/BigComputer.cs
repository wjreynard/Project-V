using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BigComputer : MonoBehaviour
{
    public int counter = 0;
    public bool bActive = false;
    public bool bReady = false;
    public Animator animator;
    public EndScreen endScreen;

    public GameObject[] counters = new GameObject[3];

    public AudioMixer audioMixer_Music;
    public AudioManager audioManager_Effects;

    IEnumerator EndScreenCoroutine()
    {
        yield return new WaitForSeconds(5.0f);

        // should be activated after final big computer animation finishes
        endScreen.bActive = true;
    }

    private void Update()
    {
        if (counter >= 3)
        {
            bReady = true;

            StartCoroutine(FadeMixerGroup.StartFade(audioMixer_Music, "MusicMasterVolume", 1.0f, 0.0f));
            GetComponent<AudioSource>().minDistance = 60.0f;
        }

        DisplayCounters();

        if (bActive)
        {
            GetComponent<AudioSource>().volume = 0.0f;
            animator.SetBool("Active", true);
            StartCoroutine(EndScreenCoroutine());
        }
    }

    private void DisplayCounters()
    {
        if (counter >= 1)
        {
            counters[0].SetActive(true);
        }

        if (counter >= 2)
        {
            counters[1].SetActive(true);
        }

        if (counter >= 3)
        {
            counters[2].SetActive(true);
        }

    }
}
