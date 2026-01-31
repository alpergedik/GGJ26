using System;
using UnityEngine;

public class Hook : MonoBehaviour
{   
    private float hookSpeed = 10f;
    private float releaseBoost = 0.6f;
    public Rigidbody2D rb;
    public LineRenderer lineRenderer;
    public static Transform playerPosition;
    private DistanceJoint2D distanceJoint;
    private Rigidbody2D playerRB;
    private PlayerMovement playerMovement;
    
    
    

    private void Awake()
    {
        playerMovement = PlayerMovement.sendRB().GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        playerRB = PlayerMovement.sendRB();
    }

    void Start()
    {   
        
        distanceJoint = GetComponent<DistanceJoint2D>();
        distanceJoint.enabled = false;
        lineRenderer = GetComponent<LineRenderer>();
        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.x = rotation.x * PlayerMovement.facingDirection;
        rb.linearVelocity =rotation * hookSpeed;
        

    }

    // Update is called once per frame
    void Update()
    {
        
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, playerPosition.position);
        if (Input.GetKeyUp(KeyCode.R)||playerMovement.isGrounded)
        {   
            ReleaseWithMomentum();
            Destroy(gameObject);
            
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            return;}
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.bodyType = RigidbodyType2D.Kinematic;
        playerMovement.isSwinging = true;
        distanceJoint.connectedAnchor = Vector2.zero;
        distanceJoint.connectedBody = playerRB;
        distanceJoint.distance = Vector2.Distance(playerRB.position, rb.position);
        distanceJoint.enabled = true;

    }
    public void ReleaseWithMomentum()
    {
        if (distanceJoint.enabled)
        {
            Vector2 releaseVelocity = playerRB.linearVelocity;
            Vector2 boost = new Vector2(releaseVelocity.x, releaseVelocity.y * 0.3f);
            playerMovement.isSwinging = false;
            playerMovement.doubleJump = true;

            playerRB.AddForce(boost * releaseBoost, ForceMode2D.Impulse);

            distanceJoint.enabled = false;
            distanceJoint.distance = 0f;
            
        }

    }
}
