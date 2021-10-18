using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] public GameObject[] healthBar;
    [SerializeField] private int health = 4;
    
    // Start is called before the first frame update
    void Start()
    {
        health = 4;
        
        foreach (GameObject healthPoint in healthBar)
        {
            healthPoint.GetComponent<Image>().color = Color.black;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            // Debug.Log("Game Over!");
            GetComponent<PlayerLives>().DecrementLives();

            health = 4;
        }

        for (int i = 0; i <= health - 1; i++)
        {
            healthBar[i].GetComponent<Image>().color = Color.white;
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

    public void GainHealth(int amount)
    {
        health += amount;
        
        if (health > 4)
        {
            health = 4;
            healthBar[health - 1].GetComponent<Image>().color = Color.white;
        }
    }

    public void InstaKill()
    {
        health = 0;
    }
}
