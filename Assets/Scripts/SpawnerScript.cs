using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem.Controls;


public class SpawnerScript : MonoBehaviour
{
    public GameObject spawnedObject;
    private float i = 8;
    public int spawnTime;
    private int spawnedCount;
    public int maxSpawnCount;
    public GameObject spawnerInstance;
   
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
        Instantiate(spawnedObject, new Vector3()  { x = spawnerInstance.transform.position.x-1,  y= spawnerInstance.transform.position.y, z = spawnerInstance.transform.position.z}, Quaternion.identity);
    }
}
