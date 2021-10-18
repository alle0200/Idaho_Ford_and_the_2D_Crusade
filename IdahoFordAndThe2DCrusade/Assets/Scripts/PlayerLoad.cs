using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class PlayerLoad : MonoBehaviour
{
    // private Transform playerLocation;
    // protected Transform lastSavedPlayerPosition;
    // protected int health;
    // protected int lives;

    [SerializeField] private Transform player;
    [SerializeField] private Vector3 resetPoint;

    // public PlayerSave()
    // {
    //     health = 4;
    //     lives = 3;
    // }
    //
    // public PlayerSave(Transform location, int remainingLives)
    // {
    //     playerLocation = location;
    //     lives = remainingLives;
    // }

    //code semi-inspired by https://www.youtube.com/watch?v=6uMFEM-napE
    public void Save()
    {
        if (File.Exists(Application.dataPath + "/save.txt"))
        {
            SavePlayer playerSave = new SavePlayer()
            {
                remainingLives = player.GetComponent<PlayerLives>().GetRemainingLives(),
                playerLocation = player.transform.position
            };
            
            // found this code snippet on
            // https://www.codegrepper.com/code-examples/csharp/JsonSerializationException%3A+Self+referencing+loop+detected+for+property+%27normalized%27+with+type+%27UnityEngine.Vector3%27
            var settings = new Newtonsoft.Json.JsonSerializerSettings();
            // This tells your serializer that multiple references are okay.
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        
            string serializedPlayer = JsonConvert.SerializeObject(playerSave, settings);
        
            File.WriteAllText(Application.dataPath + "/save.txt", serializedPlayer);
        }
    }

    public void SaveLivesOnly()
    {
        if (File.Exists(Application.dataPath + "/save.txt"))
        {
            string serializedPlayerLoad = File.ReadAllText(Application.dataPath + "/save.txt");
            SavePlayer playerLoad = JsonConvert.DeserializeObject<SavePlayer>(serializedPlayerLoad);
            
            SavePlayer playerSave = new SavePlayer()
            {
                remainingLives = player.GetComponent<PlayerLives>().GetRemainingLives(),
                playerLocation = playerLoad.playerLocation
            };

            // found this code snippet on
            // https://www.codegrepper.com/code-examples/csharp/JsonSerializationException%3A+Self+referencing+loop+detected+for+property+%27normalized%27+with+type+%27UnityEngine.Vector3%27
            var settings = new Newtonsoft.Json.JsonSerializerSettings();
            // This tells your serializer that multiple references are okay.
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        
            string serializedPlayer = JsonConvert.SerializeObject(playerSave, settings);
            
            Debug.Log(serializedPlayer);
        
            File.WriteAllText(Application.dataPath + "/save.txt", serializedPlayer);
        }
    }

    public void Load()
    {
        if (File.Exists(Application.dataPath + "/save.txt"))
        {
            string serializedPlayer = File.ReadAllText(Application.dataPath + "/save.txt");
            SavePlayer playerLoad = JsonConvert.DeserializeObject<SavePlayer>(serializedPlayer);
            
            player.GetComponent<PlayerMovement>().SetPosition(playerLoad.playerLocation);
            player.GetComponent<PlayerLives>().SetRemainingLives(playerLoad.remainingLives);
        }
    }

    public void Reset()
    {
        if (File.Exists(Application.dataPath + "/save.txt"))
        {
            SavePlayer playerSave = new SavePlayer()
            {
                remainingLives = 3,
                playerLocation = resetPoint
            };
            
            // found this code snippet on
            // https://www.codegrepper.com/code-examples/csharp/JsonSerializationException%3A+Self+referencing+loop+detected+for+property+%27normalized%27+with+type+%27UnityEngine.Vector3%27
            var settings = new Newtonsoft.Json.JsonSerializerSettings();
            // This tells your serializer that multiple references are okay.
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        
            string serializedPlayer = JsonConvert.SerializeObject(playerSave, settings);
        
            File.WriteAllText(Application.dataPath + "/save.txt", serializedPlayer);
            
            Load();
        }
    }
    
    private class SavePlayer
    {
        public int remainingLives;
        public Vector3 playerLocation;
    }
    
    // everytime a player passes a new checkpoint, create a new player variable and save it
    // save the location of the checkpoint they passed, and how many lives they have left
    // when they die, load that player variable
    
    // Start is called before the first frame update
    void Start()
    {
        Load();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
    }
}
