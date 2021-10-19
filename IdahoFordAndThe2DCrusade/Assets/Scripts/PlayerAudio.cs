using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip hurtClip;
    // [SerializeField] private AudioClip invisibleClip;
    [SerializeField] private AudioClip heartClip;
    [SerializeField] private AudioClip lifeClip;
    [SerializeField] private AudioClip checkpointClip;
    [SerializeField] private AudioClip goalClip;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayJumpClip()
    {
        GetComponent<AudioSource>().clip = jumpClip;
        GetComponent<AudioSource>().Play();
    }

    public void PlayHurtClip()
    {
        GetComponent<AudioSource>().clip = hurtClip;
        GetComponent<AudioSource>().Play();
    }
    
    // public void PlayInvisibleClip()
    // {
    //     GetComponent<AudioSource>().clip = invisibleClip;
    //     GetComponent<AudioSource>().Play();
    // }
    
    public void PlayHeartClip()
    {
        GetComponent<AudioSource>().clip = heartClip;
        GetComponent<AudioSource>().Play();
    }
    
    public void PlayLifeClip()
    {
        GetComponent<AudioSource>().clip = lifeClip;
        GetComponent<AudioSource>().Play();
    }

    public void PlayCheckpointClip()
    {
        GetComponent<AudioSource>().clip = checkpointClip;
        GetComponent<AudioSource>().Play();
    }

    public void PLayGoalClip()
    {
        GetComponent<AudioSource>().clip = goalClip;
        GetComponent<AudioSource>().Play();
    }
    
}
