using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLives : MonoBehaviour
{
    [SerializeField] private Text livesCounter;
    [SerializeField] private int remainingLives;
    [SerializeField] private GameObject gameMaster;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        livesCounter.text = remainingLives + "x";
    }

    public void DecrementLives()
    {
        gameMaster.GetComponent<PlayerLoad>().Load();
        remainingLives--;
        gameMaster.GetComponent<PlayerLoad>().SaveLivesOnly();
    }

    public void IncrementLives()
    {
        remainingLives++;
    }

    public int GetRemainingLives()
    {
        return remainingLives;
    }

    public void SetRemainingLives(int livesNumber)
    {
        remainingLives = livesNumber;
    }
}
