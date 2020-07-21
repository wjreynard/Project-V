using UnityEngine;

public class Hazard : MonoBehaviour
{
    public Vector2 velocity = new Vector2(1, 0);
    private Rigidbody2D rb2d;
    public float moveSpeed = 3.0f;
    private Vector2 origin;
    public float offset = 0;
    
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        origin = rb2d.position;
    }
    private void FixedUpdate()
    {
        rb2d.position = origin + velocity * Mathf.Sin((offset * Mathf.PI + Time.time) * moveSpeed);
    }
}
