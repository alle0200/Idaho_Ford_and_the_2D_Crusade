using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AirEnemy : Enemy
{
    [SerializeField] private float detectionRadius = 4;

    private float seconds;
    [SerializeField] private float minSeconds = 0;
    [SerializeField] private float maxSeconds = 5;

    private float moveX;
    private float moveY;
    [SerializeField] private float minMove = -1;
    [SerializeField] private float maxMove = 1;

    private bool isCoroutineRunning = false;
    private bool canSeePlayer = false;
    private Vector3 newMoveDirection;

    private LayerMask enemyLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        enemyLayerMask = ~(1 << 7);
        StartCoroutine(RandomMovement());
        isCoroutineRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    public override void Move()
    {
        if (Vector3.Distance(transform.position, player.position) <= detectionRadius)
        {
            GetComponent<Animator>().SetBool("CanSeePlayer", true);
        }

        else
        {
            GetComponent<Animator>().SetBool("CanSeePlayer", false);
        }

        canSeePlayer = GetComponent<Animator>().GetBool("CanSeePlayer");

        if (canSeePlayer)
        {
            StopCoroutine(RandomMovement());
            isCoroutineRunning = false;
        }

        else
        {
            transform.position += newMoveDirection * Time.fixedDeltaTime * moveSpeed;
            
            if (isCoroutineRunning == false)
            {
                StartCoroutine(RandomMovement());
                isCoroutineRunning = true;
            }
        }
    }

    IEnumerator RandomMovement()
    {
        while (!canSeePlayer)
        {
            seconds = Random.Range(minSeconds, maxSeconds);
            moveX = Random.Range(minMove, maxMove);
            moveY = Random.Range(minMove, maxMove);
            
            if (moveX > 0 && !movingRight)
            {
                TurnAround();
            }
            
            if (moveX < 0 && movingRight)
            {
                TurnAround();
            }

            newMoveDirection = new Vector3(moveX, moveY, 0);
            
            yield return new WaitForSeconds(seconds);
        }
        
        yield return null;
    }
}
