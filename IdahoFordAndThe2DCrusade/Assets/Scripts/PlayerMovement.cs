using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private float speed = 1;
    [SerializeField] private float maxVelocity = 5;
    [SerializeField] private Vector2 jumpForce;

    private float horizontalMovement = 0f;
    private Vector3 currentVelocity = Vector3.zero;
    private bool isJumping = false;
    private bool isGrounded = true;
    
    // Start is called before the first frame update

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
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
        horizontalMovement = Input.GetAxisRaw("Horizontal") * speed * Time.fixedDeltaTime;

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
        
    }
}
