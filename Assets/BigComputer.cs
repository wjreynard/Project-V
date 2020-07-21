using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigComputer : MonoBehaviour
{
    public int counter = 0;
    public bool bActive = false;
    public bool bReady = false;
    public Animator animator;
    public EndScreen endScreen;

    IEnumerator EndScreenCoroutine()
    {
        yield return new WaitForSeconds(0.5f);

        // should be activated after final big computer animation finishes
        endScreen.bActive = true;
    }

    private void Update()
    {
        if (counter >= 4)
        {
            bReady = true;
        }

        if (bActive)
        {
            animator.SetBool("Active", true);
            StartCoroutine(EndScreenCoroutine());
        }
    }
}
