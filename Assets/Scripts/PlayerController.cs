using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PlayerController : PhysicsObject
{
    [Space(20)]

    public float maxSpeed = 10.0f;
    public bool bCanMove = false;
    public Animator animator;
    public int score = 0;
    public int deathCount = 0;
    private bool bAlreadyLanded = true;

    [Space(20)]

    public Checkpoint checkpoint;
    public Vector3 checkpointTransform;
    public GameObject activeCamera;

    [Space(20)]

    public AudioManager audioManager_Effects;
    public AudioManager audioManager_Music;
    public AudioMixer audioMixer_Music;

    [Space(20)]

    public RespawnImage respawnImage;

    [Space(20)]

    public BigComputer bigComputer;

    public Vector2 colliderOffset;

    protected override void ComputeVelocity()
    {
        if (bCanMove)
        {
            Vector2 move = Vector2.zero;
            move.x = Input.GetAxisRaw("Horizontal");

            // flip gravity
            if (Input.GetButtonDown("Jump") && (velocity.y < 0.01f && velocity.y > -0.01f))
            {
                FlipGravity();
                audioManager_Effects.PlaySound("Jump");
            }

            // play landing sound and fade in and out the beat track
            if (!bAlreadyLanded && velocity.y < 0.01f && velocity.y > -0.01f)  // if not moving on y-axis
            {
                bAlreadyLanded = true;
                audioManager_Effects.PlaySound("Landed");
                StartCoroutine(FadeMixerGroup.StartFade(audioMixer_Music, "BeatMasterVolume", 0.4f, 0.0f));
            }
            else if (velocity.y > 0.01f || velocity.y < -0.01f) // if moving up or down
            {
                bAlreadyLanded = false;
                StartCoroutine(FadeMixerGroup.StartFade(audioMixer_Music, "BeatMasterVolume", 0.01f, 1.0f));
            }

            // flip sprite
            bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < -0.01f));
            if (flipSprite) spriteRenderer.flipX = !spriteRenderer.flipX;

            targetVelocity = move * maxSpeed;

            if ((move.x > 0.01f) || (move.x < -0.01f))
            {
                animator.SetBool("Moving", true);
            } else
            {
                animator.SetBool("Moving", false);
            }
        }
    }

    private void Start()
    {
        animator.SetBool("Flashing", false);
    }

    IEnumerator RespawnCoroutine()
    {
        respawnImage.Activate();

        bCanMove = false;
        animator.SetBool("Flashing", true);

        yield return new WaitForSeconds(0.5f);

        rb2d.position = checkpointTransform;
        activeCamera.SetActive(false);
        activeCamera = checkpoint.thisCamera;
        activeCamera.SetActive(true);

        int r = Mathf.CeilToInt(UnityEngine.Random.Range(0.0f, 3.0f));
        audioManager_Effects.PlaySound("Respawn" + r.ToString());
        bCanMove = true;

        yield return new WaitForSeconds(1.0f);
        animator.SetBool("Flashing", false);
    }

    private void ResetPlayer()
    {
        Vector2 oldGravityVector = gravityVector;
        float oldGravityModifier = gravityModifier;
        gravityModifier = 0;
        velocity *= 0;
        targetVelocity *= 0;

        StartCoroutine(RespawnCoroutine());

        gravityVector = oldGravityVector;
        gravityModifier = oldGravityModifier;

        deathCount++; // glitch with respawning twice
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Spikes"))
        {
            ResetPlayer();
        }
        else if (collision.gameObject.CompareTag("Coin"))
        {
            collision.gameObject.SetActive(false);
            score += 1;
            audioManager_Effects.PlaySound("Coin");
        }
        else if (collision.gameObject.CompareTag("Checkpoint"))
        {
            if (collision.gameObject.GetComponent<Checkpoint>().bActive == false)
            {
                int r = Mathf.CeilToInt(UnityEngine.Random.Range(0.0f, 3.0f));
                audioManager_Effects.PlaySound("Checkpoint" + r.ToString());
            }

            // reset old checkpoint
            if (checkpoint != null)
            {
                checkpoint.bActive = false;
            }

            // set new checkpoint
            checkpoint = collision.gameObject.GetComponent<Checkpoint>();
            checkpointTransform = collision.gameObject.GetComponent<Checkpoint>().childTransform.position;
            collision.gameObject.GetComponent<Checkpoint>().bActive = true;
        }
        else if (collision.gameObject.CompareTag("Switcher"))
        {
            FlipGravity();
            audioManager_Effects.PlaySound("Switcher");
        }
        else if (collision.gameObject.CompareTag("Computer"))
        {
            if (collision.gameObject.GetComponent<Computer>().bActive == false)
            {
                audioManager_Effects.PlaySound("Computer");
                bigComputer.counter++;
            }

            collision.gameObject.GetComponent<Computer>().bActive = true;
        }
        else if (collision.gameObject.CompareTag("BigComputer"))
        {
            if (collision.gameObject.GetComponent<BigComputer>().bReady)
            {
                collision.gameObject.GetComponent<BigComputer>().bActive = true;
                bCanMove = false;
                audioManager_Effects.PlaySound("BigComputer");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bounds"))
        {
            Debug.Log("Left bounds");
            ResetPlayer();
        }
    }

    void FinishLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1) {
            SceneManager.LoadScene(0);
        } else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void FlipGravity()
    {
        colliderOffset *= -1;
        GetComponent<BoxCollider2D>().offset = colliderOffset;

        velocity.y *= 0;
        gravityModifier *= -1;
        spriteRenderer.flipY = !spriteRenderer.flipY;

    }
}
