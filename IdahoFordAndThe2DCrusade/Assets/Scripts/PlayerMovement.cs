using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform CeilingCheck;
    [SerializeField] private float checkRadius;
    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private BoxCollider2D playerHitbox;
    [SerializeField] private float speed = 1;
    [SerializeField] private float crouchSpeed = 0.75f;
    [SerializeField] private float maxVelocity = 5;
    [SerializeField] private Vector2 jumpForce;

    private float horizontalMovement = 0f;
    private Vector3 currentVelocity = Vector3.zero;
    private bool isJumping = false;
    private bool isGrounded = true;
    private bool isCrouching = false;
    private bool canPhase = false;
    private bool isInvisible = false;

    [SerializeField] private Slider ghostSlider;
    
    private Color ghostColor = new Color(1, 1, 1, 0.5f);
    private Color solidColor = new Color(1, 1, 1, 1);
    private Color transitionColor;
    // private float colorVelocity = 0f;
    [SerializeField] private float ghostColorTransTime = 0f;
    [SerializeField] private float solidColorTransTime = 0f;
    [SerializeField] private float colorTransIncrement = 0.2f;
    
    private LayerMask ceilingLayerMask;
    // private LayerMask groundLayerMask;
    // private LayerMask invisibleLayerMask;
    
    // Start is called before the first frame update

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        ceilingLayerMask = ~(1 << 3);
    }

    void Start()
    {
        Debug.Log("I'm here!");
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("I'm here!");
        
        // MoveHorizontal();
        
        // Debug.Log(playerRigidbody.velocity);
        
        Jump();
        Crouch();
        Invisible();
    }

    void FixedUpdate()
    {
        // if (Input.GetKey(KeyCode.LeftArrow))
        // {
        //     if (Math.Abs(playerRigidbody.velocity.x) <= maxVelocity)
        //     {
        //         playerRigidbody.AddForce(Vector2.left * speed * Time.fixedDeltaTime);
        //     }
        // }
        //
        // if (Input.GetKey(KeyCode.RightArrow))
        // {
        //     if (Math.Abs(playerRigidbody.velocity.x) <= maxVelocity)
        //     {
        //         playerRigidbody.AddForce(Vector2.right * speed * Time.fixedDeltaTime);
        //     }
        // }
        
        Move();

        Collider2D[] colliders = new Collider2D[100];
        int numberOfContacts = playerRigidbody.GetContacts(colliders);
        isGrounded = numberOfContacts > 0;

        if (isGrounded && isJumping)
        {
            playerRigidbody.AddForce(jumpForce);
            isGrounded = false;
            isJumping = false;
        }
    }

    private void Move()
    {
        if (isCrouching)
        {
            horizontalMovement = Input.GetAxisRaw("Horizontal") * speed * Time.fixedDeltaTime * crouchSpeed;
        }

        else
        {
            horizontalMovement = Input.GetAxisRaw("Horizontal") * speed * Time.fixedDeltaTime;
        }

        Vector3 targetVelocity = new Vector3(horizontalMovement, playerRigidbody.velocity.y);
        playerRigidbody.velocity =
            Vector3.SmoothDamp(playerRigidbody.velocity, targetVelocity, ref currentVelocity, maxVelocity);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            isJumping = true;
        }
    }

    private void Crouch()
    {
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || Physics2D.OverlapCircle(CeilingCheck.position, checkRadius, ceilingLayerMask))
        {
            playerHitbox.enabled = false;
            isCrouching = true;
        }

        else
        {
            // Debug.Log("Not Crouching");
                playerHitbox.enabled = true;
                isCrouching = false;
        }
    }

    private void Invisible()
    {
        if (ghostSlider.value > 0)
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                isInvisible = true;
            
                solidColorTransTime = 0;
                Debug.Log("Invisible");
                Physics2D.IgnoreLayerCollision(3, 6, true);
                ceilingLayerMask = ~((1 << 3) | (1 << 6));

                transitionColor = Color.Lerp(GetComponent<SpriteRenderer>().color, ghostColor, ghostColorTransTime);
                ghostColorTransTime += colorTransIncrement * Time.deltaTime;
                GetComponent<SpriteRenderer>().color = transitionColor;
            }
        }

        else
        {
            isInvisible = false;
            
            ghostColorTransTime = 0;
            Debug.Log("Not Invisible");
            Physics2D.IgnoreLayerCollision(3, 6, false);
            ceilingLayerMask = ~(1 << 3);
            
            transitionColor = Color.Lerp(GetComponent<SpriteRenderer>().color, solidColor, solidColorTransTime);
            solidColorTransTime += colorTransIncrement * Time.deltaTime;
            GetComponent<SpriteRenderer>().color = transitionColor;
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 hitboxCenter = new Vector3(transform.position.x, transform.position.y + (playerHitbox.offset.y * 2), 0);
        Vector3 hitboxSize =
            new Vector3(playerHitbox.size.x, playerHitbox.size.y * transform.localScale.y, 0);
        
        if (playerHitbox.enabled)
        {
            Gizmos.color = Color.green;
        }

        else
        {
            Gizmos.color = Color.clear;
        }
        
        Gizmos.DrawCube(hitboxCenter, hitboxSize);
    }

    public bool GetVisibility()
    {
        return isInvisible;
    }
}
