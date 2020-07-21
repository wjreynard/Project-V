using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    // built from:
    // https://www.youtube.com/playlist?list=PLX2vGYjWbI0SUWwVPCERK88Qw8hpjEGd8

    public float minGroundNormalY = 0.65f;
    public float gravityModifier = 5.0f;
    public Vector2 gravityVector = new Vector2(0, -10.0f);


    protected SpriteRenderer spriteRenderer;

    protected Vector2 targetVelocity;

    protected bool bGrounded;
    protected Vector2 groundNormal;

    protected Vector2 velocity;
    protected Rigidbody2D rb2d;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);

    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.01f;


    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));  // get layer collision mask from this gameobject's layer
        contactFilter.useLayerMask = true;
    }

    private void Update()
    {
        targetVelocity = Vector2.zero;  // clear old data
        ComputeVelocity();
    }

    protected virtual void ComputeVelocity()
    {

    }

    private void FixedUpdate()
    {
        //velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        velocity += gravityModifier * gravityVector * Time.deltaTime;   // note: it is possible to change Physics2D.gravity via script which may be useful if other objects need to be affected by gravity

        velocity.x = targetVelocity.x;
        //velocity = targetVelocity;

        bGrounded = false;  // false until collision registered

        Vector2 deltaPosition = velocity * Time.deltaTime;

        // call Movement for horizontal movement
        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);
        Vector2 move = moveAlongGround * deltaPosition.x;
        Movement(move, false);
        // call Movement for vertical movement
        move = Vector2.up * deltaPosition.y;
        Movement(move, true);
    }

    void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;
        if (distance > minMoveDistance)
        {
            int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);

            hitBufferList.Clear();  // clear old data

            // fill list with objects that overlap our collider
            for (int i = 0; i < count; i++)
            {
                hitBufferList.Add(hitBuffer[i]);
            }

            // check normal for each of those hit objects to determine angle with thing we're colliding with
            for (int i = 0; i < hitBufferList.Count; i++)
            {
                Vector2 currentNormal = hitBufferList[i].normal;

                // check normal to avoid setting grounded when hitting ceiling
                if (currentNormal.y > minGroundNormalY)
                {
                    bGrounded = true;

                    if (yMovement)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                // calculate dot product of velocity and currentNormal and store in projection
                // determine whether we need to subtract from current velocity to prevent player entering another collider
                float projection = Vector2.Dot(velocity, currentNormal);
                if (projection < 0)
                {
                    // cancel out the part of the velocity that would be stopped by the collision
                    velocity = velocity - projection * currentNormal;
                }

                float modifiedDistance = hitBufferList[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        rb2d.position = rb2d.position + move.normalized * distance;
    }
}
