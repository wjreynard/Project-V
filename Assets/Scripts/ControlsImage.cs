using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsImage : MonoBehaviour
{
    public Animator animator;
    public float duration = 2.5f;

    public PlayerController player;

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
        if (Input.GetButtonDown("Jump"))
        {
            animator.SetBool("Active", false);
            player.bCanMove = true;
        }
    }
}
