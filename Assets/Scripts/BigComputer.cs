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

    public GameObject[] counters = new GameObject[3];

    IEnumerator EndScreenCoroutine()
    {
        yield return new WaitForSeconds(5.0f);

        // should be activated after final big computer animation finishes
        endScreen.bActive = true;
    }

    private void Update()
    {
        if (counter >= 3)
        {
            bReady = true;
        }

        DisplayCounters();

        if (bActive)
        {
            animator.SetBool("Active", true);
            StartCoroutine(EndScreenCoroutine());
        }
    }

    private void DisplayCounters()
    {
        if (counter >= 1)
        {
            counters[0].SetActive(true);
        }

        if (counter >= 2)
        {
            counters[1].SetActive(true);
        }

        if (counter >= 3)
        {
            counters[2].SetActive(true);
        }

    }
}
