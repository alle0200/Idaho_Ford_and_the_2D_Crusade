using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour, IHealing
{
    [SerializeField] private GameObject gameMaster;
    [SerializeField] private int healAmount = 2;

    public void HealPlayer(GameObject player)
    {
        player.GetComponent<HealthBar>().GainHealth(HealingAmount);
    }

    public int HealingAmount
    {
        get;
        set;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        HealingAmount = healAmount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            Debug.Log("Tagged!");
            gameMaster.GetComponent<PlayerLoad>().Save();
            
            HealPlayer(other.gameObject);
        }
    }
}
