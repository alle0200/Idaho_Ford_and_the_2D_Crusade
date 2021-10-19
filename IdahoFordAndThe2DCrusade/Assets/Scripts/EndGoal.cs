using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGoal : MonoBehaviour
{
    private bool checkpointVisited = false;
    [SerializeField] private GameObject gameMaster;

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
        if (other.gameObject.tag == "Player")
        {
            
            // Debug.Log("Tagged!");
            // gameMaster.GetComponent<PlayerLoad>().Save();

            if (checkpointVisited == false)
            {
                other.gameObject.GetComponent<PlayerAudio>().PLayGoalClip();
                other.gameObject.GetComponent<PlayerMovement>().enabled = false;
                other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

                StartCoroutine(Wait());
                
                checkpointVisited = true;
            }
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("WinScreen");
    }
}
