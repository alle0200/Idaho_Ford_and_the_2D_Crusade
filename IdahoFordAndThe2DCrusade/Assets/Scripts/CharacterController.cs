using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float movementSpeed;
    [SerializeField] private Transform characterCenter;
    [SerializeField] private Rigidbody2D characterRigidbody;

    private bool isGrounded = true;
    private bool isJumping = false;
    // private bool movingRight;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
