using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Transform playerLocation;
    protected Transform lastSavedPlayerPosition;
    protected int health;
    protected int lives;

    public Player()
    {
        health = 4;
        lives = 3;
    }

    public Player(Transform location, int remainingLives)
    {
        playerLocation = location;
        lives = remainingLives;

        health = 4;
        lastSavedPlayerPosition = location;
    }
    
    // everytime a player passes a new checkpoint, create a new player variable and save it
    // save the location of the checkpoint they passed, and how many lives they have left
    // when they die, load that player variable
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
