using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] public GameObject[] healthBar;
    [SerializeField] private int health = 4;

    private bool isCoroutineRunning = false;

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
        if (health <= 0 && isCoroutineRunning == false)
        {
            // Debug.Log("Game Over!");
            StartCoroutine(Pause());
            isCoroutineRunning = true;
            // GetComponent<PlayerLives>().DecrementLives();

            // health = 4;
        }

        for (int i = 0; i <= health - 1; i++)
        {
            healthBar[i].GetComponent<Image>().color = Color.white;
        }
    }

    IEnumerator Pause()
    {
        for (int i = 0; i <= 4 - 1; i++)
        {
            healthBar[i].GetComponent<Image>().color = Color.black;
        }
        
        GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<PlayerMovement>().enabled = false;
        
        yield return new WaitForSeconds(1);
        
        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<PlayerLives>().DecrementLives();
        
        health = 4;
        isCoroutineRunning = false;
    }

    public void LoseHealth()
    {
        if (health > 0)
        {
            healthBar[health - 1].GetComponent<Image>().color = Color.black;
            health--;
            GetComponent<PlayerAudio>().PlayHurtClip();
        }
    }

    public void GainHealth(int amount)
    {
        health += amount;
        // GetComponent<PlayerAudio>().PlayHeartClip();
        
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
