using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsImage : MonoBehaviour
{
    public Animator animator;
    public float duration = 2.5f;

    IEnumerator FadeControls()
    {
        //Debug.Log("Started Coroutine at timestamp : " + Time.time);

        yield return new WaitForSeconds(duration);
        animator.SetBool("Active", false);

        //Debug.Log("Ended Coroutine at timestamp : " + Time.time);
    }

    void Start()
    {
        StartCoroutine(FadeControls());
    }
}
