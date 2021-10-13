using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostPowerSlider : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float decrementValue = 0.1f;
    [SerializeField] private float incrementValue = 0.1f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetSliderValue()
    {
        return GetComponent<Slider>().value;
    }

    public void IncrementSlider()
    {
        if (player.GetComponent<PlayerMovement>().GetVisibility() == true && GetComponent<Slider>().value > 0)
        {
            GetComponent<Slider>().value -= decrementValue * Time.deltaTime;
        }
    }

    public void DecrementSlider()
    {
        if (player.GetComponent<PlayerMovement>().GetVisibility() == false && GetComponent<Slider>().value < 1)
        {
            GetComponent<Slider>().value += incrementValue * Time.deltaTime;
        }
    }
    
}
