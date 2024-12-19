using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SocialPlatforms;


public class SpawnerScript : MonoBehaviour
{
    public GameObject spawnedObjectType;
    public GameObject spawnedObjectType2;
    private float i = 8;
    public int spawnTime;
    private int spawnedCount;
    public int maxSpawnCount;
    public GameObject spawnerInstance;
    private float xDeviation; 
    private float yDeviation;
    private int spawnType;
   
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        xDeviation = UnityEngine.Random.Range(-1.0f, 1.0f);
        yDeviation = UnityEngine.Random.Range(-1.0f, 1.0f);
        spawnedCount = GameObject.FindGameObjectsWithTag("Spawner").Length; 
        if (spawnedCount < maxSpawnCount && i >= spawnTime) 
        {
            SpawnObject();
            i = 0;
        }
        else
        {
            i += Time.deltaTime;
        }
       
    }

    void SpawnObject()
    {
        spawnType = Random.Range(0, 10);
        if (spawnType < 6)
        {
            Instantiate(spawnedObjectType, new Vector3()  { x = spawnerInstance.transform.position.x - xDeviation,  y = spawnerInstance.transform.position.y - yDeviation, z = spawnerInstance.transform.position.z}, Quaternion.identity);
        }
        else
        {
            Instantiate(spawnedObjectType2, new Vector3()  { x = spawnerInstance.transform.position.x - xDeviation,  y = spawnerInstance.transform.position.y - yDeviation, z = spawnerInstance.transform.position.z}, Quaternion.identity);
        }
        
    }
}
