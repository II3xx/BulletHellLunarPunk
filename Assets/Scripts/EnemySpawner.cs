using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyStats stats;
    [SerializeField] bool isRandom;
    [SerializeField] List<GameObject> PrefabsToSpawn;
    [SerializeField] [Range(0.5f, 10)] float timeToSpawn;
    [SerializeField] [Range(1,15)] int spawnLimit;
    float runTime = 0;
    int orderToSpawn = 0;
    List<GameObject> spawnedList;
    [SerializeField] [Range(1, 5)] int spawnAmount;

    // Start is called before the first frame update
    void Start()
    {
        PrefabsToSpawn = new();
        spawnedList = new();
        stats = stats.CopyStats(stats);
    }

    public Faction Allegiance
    {
        get => stats.Allegiance;
    }

    public int Damage
    {
        set => stats.Damage = value;
    }

    // Update is called once per frame
    void Update()
    {
        runTime += Time.deltaTime;
        if(runTime > timeToSpawn)
        {
            for(int i = 0; i < spawnAmount; i++)
            {
                if (spawnedList.Count > spawnLimit)
                    return;
                GameObject toManipulate;
                if(isRandom)
                {
                    toManipulate = Instantiate(PrefabsToSpawn[Random.Range(0, PrefabsToSpawn.Count - 1)]);
                }
                else
                {
                    toManipulate = Instantiate(PrefabsToSpawn[orderToSpawn]);
                    orderToSpawn++;
                }
                toManipulate.transform.position = transform.position;
                spawnedList.Add(toManipulate);
            }
        }
    }
}
