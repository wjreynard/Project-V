using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Sprite spriteInActive;
    public Sprite spriteActive;
    private SpriteRenderer spriteRenderer;

    public Transform childTransform;

    public bool bActive = false;

    public GameObject thisCamera;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (bActive)
        {
            spriteRenderer.sprite = spriteActive;
        } else if (!bActive)
        {
            spriteRenderer.sprite = spriteInActive;
        }
    }
}
