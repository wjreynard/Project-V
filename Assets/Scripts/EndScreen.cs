using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class EndScreen : MonoBehaviour
{

    public bool bActive;
    public Text finalScoreText;
    public Animator animator;
    public PlayerController player;
    public GameObject holder;

    public AudioMixer audioMixer_Effects;
    //public AudioMixer audioMixer_Music;

    private void Update()
    {
        if (bActive)
        {
            finalScoreText.text = "Y0U  F0UND  " + player.score.ToString() +" / 6  C0INS";
            holder.SetActive(true);
            animator.SetBool("Active", true);

            // restart on SPACE press
            if (Input.GetButtonDown("Jump"))
            {
                //Debug.Log("Restarting game");

                // fade music
                // fade out should be shorter than the RestartGameCoroutine() WaitForSeconds()
                //StartCoroutine(FadeMixerGroup.StartFade(audioMixer_Music, "MusicMasterVolume", 4.0f, 0.0f));
                StartCoroutine(FadeMixerGroup.StartFade(audioMixer_Effects, "EffectsMasterVolume", 4.0f, 0.0f));
                //StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "MasterVolume", 4.0f, 0.0f));   

                StartCoroutine(RestartGameCoroutine());
            }
        }
    }

    IEnumerator RestartGameCoroutine()
    {
        animator.SetBool("RestartActive", true);
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
