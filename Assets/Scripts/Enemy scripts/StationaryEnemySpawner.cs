using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StationaryEnemySpawner : BaseEnemy
{
    private StationarySpawnerStats stationarySpawnerStats;
    private List<GameObject> spawnedList;
    private float runTime = 0;
    private int orderToSpawn = 0;

    // Start is called before the first frame update
    void Start()
    {
        stationarySpawnerStats = (StationarySpawnerStats)enemyStats;
        spawnedList = new();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private GameObject RandomSpawn()
    {
        return Instantiate(stationarySpawnerStats.PrefabsToSpawn[Random.Range(0, stationarySpawnerStats.PrefabsToSpawn.Length - 1)], transform.position, Quaternion.identity);
    }

    private GameObject OrderedSpawn()
    {
        return Instantiate(stationarySpawnerStats.PrefabsToSpawn[orderToSpawn], transform.position, Quaternion.identity);
    }

    private void UpdateDeadSpawns()
    {
        for(int i = spawnedList.Count - 1; i >= 0; i--)
        {
            if(spawnedList[i] == null)
            {
                spawnedList.RemoveAt(i);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDeadSpawns();
        if (spawnedList.Count >= stationarySpawnerStats.SpawnLimit)
            return;
        runTime += Time.deltaTime;
        if(runTime > stationarySpawnerStats.TimeToSpawn)
        {
            for(int i = 0; i < stationarySpawnerStats.SpawnAmount; i++)
            {
                if (spawnedList.Count >= stationarySpawnerStats.SpawnLimit)
                    break;
                runTime = 0;
                if(stationarySpawnerStats.IsRandom)
                {
                    spawnedList.Add(RandomSpawn());
                }
                else if (!stationarySpawnerStats.IsRandom)
                {
                    spawnedList.Add(OrderedSpawn());
                    orderToSpawn++;
                    if (orderToSpawn >= stationarySpawnerStats.PrefabsToSpawn.Length)
                        orderToSpawn = 0;
                }
            }
        }
    }
}
