using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesPickup : Interactables
{
    private bool pickedUp = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && pickedUp == false)
        {
            other.gameObject.GetComponent<PlayerLives>().IncrementLives();
            pickedUp = true;
            Break();
        }
    }
}
