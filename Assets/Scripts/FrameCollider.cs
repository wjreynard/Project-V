using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameCollider : MonoBehaviour
{
    // built from: https://www.youtube.com/watch?v=C1U_0iZPioo
    public Camera thisFrame;
    public Camera nextFrame;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            thisFrame.gameObject.SetActive(true);
            nextFrame.gameObject.SetActive(false);
        }
    }
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        nextFrame.gameObject.SetActive(true);
    //        thisFrame.gameObject.SetActive(false);
    //    }
    //}

    //if (frame1.activeSelf)
    //{
    //    frame2.SetActive(true);
    //    frame1.SetActive(false);
    //}
    //else if (!frame1.activeSelf)
    //{
    //    frame1.SetActive(true);
    //    frame2.SetActive(true);
    //}
}
