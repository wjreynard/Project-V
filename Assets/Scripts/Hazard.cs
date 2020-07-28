using UnityEngine;

public class Hazard : MonoBehaviour
{
    public Vector2 velocity = new Vector2(1, 0);
    private Rigidbody2D rb2d;
    public float moveSpeed = 3.0f;
    private Vector2 origin;
    public float offset = 0;
    public bool bAbsolute = false;
    
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        origin = rb2d.position;
    }
    private void FixedUpdate()
    {
        float s = Mathf.Sin((offset * Mathf.PI + Time.time) * moveSpeed);

        if (bAbsolute)
        {
            rb2d.position = origin + velocity * Mathf.Abs(s);
        } else
        {
            rb2d.position = origin + velocity * s;
        }
    }
}
