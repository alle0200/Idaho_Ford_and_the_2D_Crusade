using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : Player
{
    [SerializeField] public GameObject[] healthBar;
    // [SerializeField] private int health = 4;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Debug.Log("Game Over!");
        }
    }

    public void LoseHealth()
    {
        if (health > 0)
        {
            healthBar[health - 1].GetComponent<Image>().color = Color.black;
            health--;
        }
    }

    public void GainHealth()
    {
        if (health < 4)
        {
            healthBar[health - 1].GetComponent<Image>().color = Color.white;
            health++;
        }
    }
}
