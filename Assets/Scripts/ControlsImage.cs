using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ControlsImage : MonoBehaviour
{
    public Animator animator;
    public float duration = 2.5f;

    public PlayerController player;
    public AudioManager audioManager_Effects;
    public AudioMixer audioMixer_Effects;
    public AudioMixer audioMixer_Music;

    private bool bStarted = false;

    IEnumerator FadeControls()
    {
        //Debug.Log("Started Coroutine at timestamp : " + Time.time);
        //Debug.Log("Ended Coroutine at timestamp : " + Time.time);

        yield return new WaitForSeconds(duration);
        animator.SetBool("Active", false);
        player.bCanMove = true; 
    }

    void Start()
    {
        //StartCoroutine(FadeControls());
    }

    private void Update()
    {
        if (!bStarted && Input.GetButtonDown("Jump"))
        {
            bStarted = true;
            StartCoroutine(FadeMixerGroup.StartFade(audioMixer_Effects, "EffectsMasterVolume", 0.01f, 1.0f));
            StartCoroutine(FadeMixerGroup.StartFade(audioMixer_Music, "MusicMasterVolume", 10.0f, 1.0f));
            audioManager_Effects.PlaySound("Start");
            animator.SetBool("Active", false);
            player.bCanMove = true;
        }
    }
}
