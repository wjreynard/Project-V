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
            animator.SetBool("Active", true);
        }
    }
}
