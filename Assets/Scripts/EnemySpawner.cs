using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyStats stats;
    [SerializeField] bool isRandom;
    [SerializeField] GameObject[] PrefabsToSpawn;
    [SerializeField] [Range(0.5f, 10)] float timeToSpawn;
    [SerializeField] [Range(1,15)] int spawnLimit;
    float runTime = 0;
    int orderToSpawn = 0;
    List<GameObject> spawnedList;
    [SerializeField] [Range(1, 5)] int spawnAmount;

    // Start is called before the first frame update
    void Start()
    {
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

    private GameObject RandomSpawn()
    {
        return Instantiate(PrefabsToSpawn[Random.Range(0, PrefabsToSpawn.Length - 1)]);
    }

    private GameObject OrderedSpawn()
    {
        return Instantiate(PrefabsToSpawn[orderToSpawn]);
    }

    private GameObject SetTransformPos(GameObject gameObject)
    {
        gameObject.transform.position = transform.position;
        return gameObject;
    }

    private void UpdateDeadSpawns()
    {
        for(int i = spawnedList.Count - 1; i == 0; i--)
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
        runTime += Time.deltaTime;
        if(runTime > timeToSpawn)
        {
            for(int i = 0; i < spawnAmount; i++)
            {
                if (spawnedList.Count >= spawnLimit)
                    break;
                runTime = 0;
                if(isRandom)
                {
                    spawnedList.Add(SetTransformPos(RandomSpawn()));
                }
                else if (!isRandom)
                {
                    spawnedList.Add(SetTransformPos(OrderedSpawn()));
                    orderToSpawn++;
                    if (orderToSpawn > PrefabsToSpawn.Length)
                        orderToSpawn = 0;
                }
            }
        }
    }
}
