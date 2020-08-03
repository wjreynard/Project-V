using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    public bool bActive = false;
    public Animator animator;

    private void Update()
    {
        if (bActive)
        {
            StartCoroutine(FadeAudioSource.StartFade(GetComponent<AudioSource>(), 0.1f, 0.0f));
            animator.SetBool("Active", true);
        }
    }
}
