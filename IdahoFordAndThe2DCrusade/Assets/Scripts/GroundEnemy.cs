using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : Enemy
{
    [SerializeField] private float raycastDistance;
    private LayerMask enemyLayerMask = ~(1 << 7);
    [SerializeField] private Transform enemyPitSensor;
    [SerializeField] private float detectionRadius;
    [SerializeField] private float maxVelocity;
    
    private Vector3 currentVelocity = Vector3.zero;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    public override void Attack(Transform player)
    {
        
    }

    public override void Move()
    {
        float XDirection = movingRight ? 1 : -1;

        float horizontalMovement = XDirection * Time.fixedDeltaTime * moveSpeed;
        
        EnemyRaycast();
        
        // enemyRigidbody.AddForce((base.movingRight ? Vector2.right : Vector2.left) * Time.fixedDeltaTime * moveSpeed);
        
        Vector3 targetVelocity = new Vector3(horizontalMovement, enemyRigidbody.velocity.y);
        enemyRigidbody.velocity =
            Vector3.SmoothDamp(enemyRigidbody.velocity, targetVelocity, ref currentVelocity, maxVelocity);
    }

    private void EnemyRaycast()
    {
        Vector2 raycastDirection = movingRight ? Vector2.right : Vector2.left;
        

        RaycastHit2D raycastResult = Physics2D.Raycast(transform.position, raycastDirection, raycastDistance, enemyLayerMask);
        
        if (raycastResult.collider != null)
        {
            if (raycastResult.collider.name == "Player")
            {
                // Attack(player);
            
                Debug.Log("I see the player!");
            }
        
            else
            {
                TurnAround();
            }
        }

        if (!Physics2D.OverlapCircle(enemyPitSensor.position, detectionRadius, enemyLayerMask))
        {
            TurnAround();
        }

    }

    public override void TurnAround()
    {
        base.TurnAround();
        
        float pitSensorX = -1 * enemyPitSensor.localPosition.x;
        enemyPitSensor.localPosition = new Vector3(pitSensorX, enemyPitSensor.localPosition.y, 0);
    }
    

    private void OnDrawGizmos()
    {
        Vector3 raycastDirection = movingRight ? Vector3.right : Vector3.left;
        
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, transform.position + raycastDistance * raycastDirection);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(enemyPitSensor.position, detectionRadius);
    }
}
