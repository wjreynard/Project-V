using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnImage : MonoBehaviour
{
    public Animator animator;

    public void Activate()
    {
        animator.SetBool("Active", true);
        StartCoroutine(ResetBool());
    }

    IEnumerator ResetBool()
    {
        yield return new WaitForSeconds(1.0f);
        animator.SetBool("Active", false);
    }
}
