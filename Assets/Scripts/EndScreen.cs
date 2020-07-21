using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{

    public bool bActive;
    public Text finalScoreText;
    public Animator animator;
    public PlayerController player;
    public GameObject holder;

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
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
