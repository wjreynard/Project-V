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
            finalScoreText.text = "Y0U SC0RED: " + player.score.ToString();

            // restart on SPACE press
            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log("Restarting game");
                StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "MasterVolume", 4.0f, 0.0f));
                StartCoroutine(RestartGameCoroutine());
            }
        }
    }

    IEnumerator RestartGameCoroutine()
    {
        Debug.Log("Started Coroutine at timestamp : " + Time.time);
        animator.SetBool("RestartActive", true);
        yield return new WaitForSeconds(5.0f);
        Debug.Log("Ended Coroutine at timestamp : " + Time.time);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
