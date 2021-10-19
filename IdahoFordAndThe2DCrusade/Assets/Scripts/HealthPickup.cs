using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Interactables , IHealing
{
    [SerializeField] private int healNumber = 1;
    
    private bool pickedUp = false;
    public int HealingAmount
    {
        get;
        set;
    }

    public void HealPlayer(GameObject player)
    {
        player.GetComponent<HealthBar>().GainHealth(HealingAmount);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        HealingAmount = healNumber;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && pickedUp == false)
        {
            HealPlayer(other.gameObject);
            other.gameObject.GetComponent<PlayerAudio>().PlayHeartClip();
            pickedUp = true;
            Break();
        }
    }
}
