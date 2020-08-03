using UnityEngine;

public class HazardAbsolute : MonoBehaviour
{
    public Vector2 velocity = new Vector2(1, 0);
    private Rigidbody2D rb2d;
    public float moveSpeed = 3.0f;
    private Vector2 origin;
    public float offset = 0;
    private bool bThudPlayed = true;
    public AudioManager audioManager_Effects;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        origin = rb2d.position;
    }
    private void FixedUpdate()
    {
        float s = Mathf.Sin((offset * Mathf.PI + Time.time) * moveSpeed);

        s = Mathf.Abs(s);
        rb2d.position = origin + velocity * s;

        if (s < 0.05f && !bThudPlayed)
        {
            bThudPlayed = true;
            GetComponent<AudioSource>().Play();
        }
        else if (s > 0.05f)
        {
            bThudPlayed = false;
        }
    }
}
