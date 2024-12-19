using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyStats stats;
    [Tooltip("Determines if the spawn order is random or in order from top to bottom")]
    [SerializeField] bool isRandom;
    [SerializeField] GameObject[] PrefabsToSpawn;
    [SerializeField] [Range(0.5f, 10)] float timeToSpawn;
    [SerializeField] [Range(1,15)] int spawnLimit;
    float runTime = 0;
    int orderToSpawn = 0;
    List<GameObject> spawnedList;
    [Tooltip("How many get spawned each time it tries to spawn (does not exceed spawn limit)")]
    [SerializeField] [Range(1, 5)] int spawnAmount;
    [SerializeField] UnityEvent onDeath;

    // Start is called before the first frame update
    void Start()
    {
        spawnedList = new();
        stats = stats.CopyStats(stats);
        stats.onDeath.AddListener(OnDeath);
    }

    private void OnDeath()
    {
        onDeath.Invoke();
        Destroy(gameObject);
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
        return Instantiate(PrefabsToSpawn[Random.Range(0, PrefabsToSpawn.Length - 1)], transform.position, Quaternion.identity);
    }

    private GameObject OrderedSpawn()
    {
        return Instantiate(PrefabsToSpawn[orderToSpawn], transform.position, Quaternion.identity);
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
        if (spawnedList.Count >= spawnLimit)
            return;
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
                    spawnedList.Add(RandomSpawn());
                }
                else if (!isRandom)
                {
                    spawnedList.Add(OrderedSpawn());
                    orderToSpawn++;
                    if (orderToSpawn >= PrefabsToSpawn.Length)
                        orderToSpawn = 0;
                }
            }
        }
    }
}
