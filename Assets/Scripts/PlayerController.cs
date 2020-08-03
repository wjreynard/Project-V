using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : PhysicsObject
{
    [Space(10)]

    public float maxSpeed = 10.0f;
    public bool bCanMove = false;
    public Animator animator;
    public int score = 0;

    [Space(10)]

    public Checkpoint checkpoint;
    public Vector3 checkpointTransform;
    public GameObject activeCamera;

    [Space(10)]

    public AudioManager audioManager_Effects;
    public AudioManager audioManager_Music;

    [Space(10)]

    public RespawnImage respawnImage;

    [Space(10)]

    public BigComputer bigComputer;

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
                //audioManager_Music.gameObject.GetComponent<AudioSource>().
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

        audioManager_Effects.PlaySound("Respawn");
        bCanMove = true;

        yield return new WaitForSeconds(1.0f);
        animator.SetBool("Flashing", false);
    }

    private void ResetPlayer()
    {
        Vector2 oldGravityVector = gravityVector;
        float oldGravityModifier = gravityModifier;
        gravityModifier = 0;

        StartCoroutine(RespawnCoroutine());

        gravityVector = oldGravityVector;
        gravityModifier = oldGravityModifier;
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
            score += 100;
            audioManager_Effects.PlaySound("Coin");
        }
        else if (collision.gameObject.CompareTag("Checkpoint"))
        {
            if (collision.gameObject.GetComponent<Checkpoint>().bActive == false)
                audioManager_Effects.PlaySound("Checkpoint");

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
        velocity.y *= 0;
        gravityModifier *= -1;
        spriteRenderer.flipY = !spriteRenderer.flipY;
    }

    //private void UpdateGravity()
    //{
    //    // could use unit circle to switch gravity
    //    // increment an angle by HALF_PI, return cos(theta), sin(theta)

    //    if (gravityIncrement == 0)
    //    {
    //        // down
    //        gravityVector = new Vector2(0, -10.0f);
    //        //spriteRenderer.flipY = !spriteRenderer.flipY;
    //        transform.eulerAngles = new Vector3(0, 0, 0);
    //    } else if (gravityIncrement == 1)
    //    {
    //        // left
    //        gravityVector = new Vector2(-10.0f, 0);
    //        //spriteRenderer.flipX = !spriteRenderer.flipX;
    //        transform.eulerAngles = new Vector3(0, 0, -90.0f);
    //    }
    //    else if (gravityIncrement == 2)
    //    {
    //        // up
    //        gravityVector = new Vector2(0, 10.0f);
    //        //spriteRenderer.flipY = !spriteRenderer.flipY;
    //        transform.eulerAngles = new Vector3(0, 0, 180.0f);
    //    }
    //    else if (gravityIncrement == 3)
    //    {
    //        // right
    //        gravityVector = new Vector2(10.0f, 0);
    //        transform.eulerAngles = new Vector3(0, 0, 90.0f);
    //        //spriteRenderer.flipX = !spriteRenderer.flipX;
    //    }
    //}
}
