using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] public Rigidbody2D enemyRigidbody;
    [SerializeField] public float moveSpeed;
    [SerializeField] public Transform player;
    public bool movingRight = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void Attack(Transform player);

    public abstract void Move();

    public virtual void TurnAround()
    {
        movingRight = !movingRight;
        GetComponent<SpriteRenderer>().flipX = movingRight;
        
        Debug.Log("I'm turning around");
    }
    
}
