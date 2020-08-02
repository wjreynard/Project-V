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
    public AudioMixer audioMixer;

    private void Update()
    {
        if (bActive)
        {
            holder.SetActive(true);
            animator.SetBool("Active", true);
            finalScoreText.text = "Y0U SC0RED " + player.score.ToString() + " / 6  C0INS";

            // restart on SPACE press
            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log("Restarting game");
                StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "MasterVolume", 4.0f, 0.0f));   // fade out should be shorter than the RestartGameCoroutine() WaitForSeconds()
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
