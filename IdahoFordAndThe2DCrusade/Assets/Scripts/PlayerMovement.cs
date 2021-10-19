using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
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
    private bool isInvisible = false;

    [SerializeField] private Slider ghostSlider;

    private Color ghostColor = new Color(1, 1, 1, 0.5f);
    private Color solidColor = new Color(1, 1, 1, 1);
    private Color transitionColor;
    private float ghostColorTransTime = 0f;
    private float solidColorTransTime = 0f;
    [SerializeField] private float colorTransIncrement = 0.2f;

    [SerializeField] private LayerMask ceilingLayerMask;
    [SerializeField] private LayerMask floorLayerMask;

    [SerializeField] Transform topRightCornerFloorCheck;
    [SerializeField] Transform bottomLeftCornerFloorCheck;

    [SerializeField] private Transform topRightCornerCeilingCheck;
    [SerializeField] private Transform bottomLeftCornerCeilingCheck;

    [SerializeField] private float enemyIFrames;
    [SerializeField] private float invisibilityIFrames;

    private bool invincibilityCoroutineStarted = false;
    private bool enemyCoroutineStarted = false;

    [SerializeField] private bool movingRight = true;

    [SerializeField] private float abyssPoint;

    // Start is called before the first frame update

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        ceilingLayerMask = ~((1 << 3) | (1 << 7) | (1 << 8));
        floorLayerMask = ~((1 << 3) | (1 << 7) | (1 << 8));
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

        if (transform.position.y < abyssPoint)
        {
            playerRigidbody.velocity = Vector2.zero;
            GetComponent<HealthBar>().InstaKill();
        }
    }

    void FixedUpdate()
    {
        Move();

        bool wasGrounded = isGrounded;
        isGrounded = false;

        // Replace this code so that you can use FloorCheck instead
        // will make it to where only the bottom of the player can check for floor
        
        // Collider2D[] colliders = new Collider2D[100];
        // int numberOfContacts = playerRigidbody.GetContacts(colliders);
        // isGrounded = numberOfContacts > 0;

        // Issue: if a player phases through a wall, becomes invisible inside of the wall,
        // and then jumps, they can jump when they shouldn't be able to.
        // May need to mess with layer masks
        
        if (Physics2D.OverlapArea(topRightCornerFloorCheck.position, bottomLeftCornerFloorCheck.position, floorLayerMask))
        {
            isGrounded = true;
            if (!wasGrounded)
            {
                // OnLandEvent.Invoke();
                GetComponent<Animator>().SetBool("isJumping", false);
            }
        }
        

        if (isGrounded && isJumping)
        {
            if (playerRigidbody.velocity.y < jumpForce.y)
            {
                playerRigidbody.AddForce(jumpForce);
            }
            
            isGrounded = false;
            isJumping = false;
            GetComponent<Animator>().SetBool("isJumping", true);
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

        if (horizontalMovement > 0 && !movingRight)
        {
            TurnAround();
        }
        
        else if (horizontalMovement < 0 && movingRight)
        {
            TurnAround();
        }
        
        GetComponent<Animator>().SetFloat("Speed", Mathf.Abs(horizontalMovement));
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            isJumping = true;
            GetComponent<PlayerAudio>().PlayJumpClip();
        }
    }

    private void Crouch()
    {
        bool wasCrouching = isCrouching;
        
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || Physics2D.OverlapArea(topRightCornerCeilingCheck.position, bottomLeftCornerCeilingCheck.position, ceilingLayerMask) && isGrounded)
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
        
        // GetComponent<Animator>().SetBool("IsCrouching", isCrouching);
        
        if (wasCrouching != isCrouching)
        {
            GetComponent<Animator>().SetBool("IsCrouching", isCrouching);
        }
    }

    private void MakeInvisible()
    {
        isInvisible = true;
            
        solidColorTransTime = 0;
        Debug.Log("Invisible");
        Physics2D.IgnoreLayerCollision(3, 6, true);
        Physics2D.IgnoreLayerCollision(3, 7, true);
        ceilingLayerMask = ~((1 << 3) | (1 << 6) | (1 << 7) | (1 << 8));
        floorLayerMask = ~((1 << 3) | (1 << 6) | (1 << 7) | (1 << 8));

        transitionColor = Color.Lerp(GetComponent<SpriteRenderer>().color, ghostColor, ghostColorTransTime);
        ghostColorTransTime += colorTransIncrement * Time.deltaTime;
        GetComponent<SpriteRenderer>().color = transitionColor;
    }

    private void MakeVisible()
    {
        isInvisible = false;
            
        ghostColorTransTime = 0;
        Debug.Log("Not Invisible");
        Physics2D.IgnoreLayerCollision(3, 6, false);

        if (enemyCoroutineStarted == false)
        {
            Physics2D.IgnoreLayerCollision(3, 7, false);
        }
        
        ceilingLayerMask = ~((1 << 3) | (1 << 7) | (1 << 8));
        floorLayerMask = ~((1 << 3) | (1 << 7) | (1 << 8));
            
        transitionColor = Color.Lerp(GetComponent<SpriteRenderer>().color, solidColor, solidColorTransTime);
        solidColorTransTime += colorTransIncrement * Time.deltaTime;
        GetComponent<SpriteRenderer>().color = transitionColor;
    }

    private void Invisible()
    {
        if (ghostSlider.value > 0)
        {
            StopCoroutine(InvisibilityDamage());
            invincibilityCoroutineStarted = false;
            
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                if (GetComponent<SpriteRenderer>().color != ghostColor)
                {
                    MakeInvisible();
                }
                ghostSlider.GetComponent<GhostPowerSlider>().DecrementSlider();
            }

            else
            {
                if (GetComponent<SpriteRenderer>().color != solidColor)
                {
                    MakeVisible();
                }
                ghostSlider.GetComponent<GhostPowerSlider>().IncrementSlider();
            }
        }

        else
        {
            MakeVisible();

            if (invincibilityCoroutineStarted != true)
            {
                StartCoroutine(InvisibilityDamage());
                invincibilityCoroutineStarted = true;
            }
            

            if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.LeftShift))
            {
                StopCoroutine(InvisibilityDamage());
                invincibilityCoroutineStarted = false;
                
                ghostSlider.GetComponent<GhostPowerSlider>().IncrementSlider();
            }
        }
    }

    IEnumerator InvisibilityDamage()
    {
        while (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            MakeRed();
            
            GetComponent<HealthBar>().LoseHealth();

            yield return new WaitForSeconds(invisibilityIFrames);
        }
    }

    public bool GetVisibility()
    {
        return isInvisible;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            // Issue: if the enemy coroutine stops when a player is inside an enemy, they take double damage.
            // This is no doubt due to the two colliders that make up the player.
            // By using a bool to block off the number of coroutines started this problem is sort of fixed,
            // but the player's character kind of clips through the enemy when they take damage.
            // Not ideal, but it's at least functional for now.
            if (enemyCoroutineStarted != true)
            {
                enemyCoroutineStarted = true;
                StartCoroutine(EnemyDamage());
            }
        }
    }

    private void MakeRed()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        solidColorTransTime = 0;
        ghostColorTransTime = 0;
    }

    IEnumerator EnemyDamage()
    {
        // GetComponent<SpriteRenderer>().color = Color.Lerp(solidColor, Color.red, Mathf.PingPong(Time.time, 1));

        Physics2D.IgnoreLayerCollision(3, 7, true);

        // Debug.Log("Layer collision: " + Physics2D.GetIgnoreLayerCollision(3,7));

        MakeRed();
        
        GetComponent<HealthBar>().LoseHealth();

        yield return new WaitForSeconds(enemyIFrames);
        
        Physics2D.IgnoreLayerCollision(3, 7, false);
        
        // Debug.Log("Layer collision: " + Physics2D.GetIgnoreLayerCollision(3,7));

        enemyCoroutineStarted = false;

        yield return null;
    }

    public void SetPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

    public virtual void TurnAround()
    {
        movingRight = !movingRight;
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
        
        Debug.Log("I'm turning around");
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
        
        Gizmos.color = Color.red;
        
        Gizmos.DrawLine(topRightCornerFloorCheck.position, bottomLeftCornerFloorCheck.position);
        
        Gizmos.color = Color.cyan;
        
        Gizmos.DrawLine(topRightCornerCeilingCheck.position, bottomLeftCornerCeilingCheck.position);
    }
}
