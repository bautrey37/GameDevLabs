using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public WaypointFollower FollowerPrefab;
    public float TimeBetweenSpawn = 0.5f;
    public float TimeBetweenWaves = 2;
    public int EnemiesInWave = 5;
    public float GameStartDelay = 3f;

    private float NextSpawnTime = 0;
    private float NextWaveTime = 0;
    private int SpawnCount;

    private Waypoint waypoint;

    void Awake()
    {
        waypoint = GetComponent<Waypoint>();
    }

    void Update()
    {
        if (Time.time < GameStartDelay) return;
        if (SpawnCount < EnemiesInWave)
        {
            if (Time.time > NextSpawnTime)
            {
                SpawnEnemy();
            }
        }
        else
        {
            if (Time.time > NextWaveTime)
            {
                StartNewWave();
            }
        }
        
    }

    void SpawnEnemy()
    {
        NextSpawnTime = Time.time + TimeBetweenSpawn;
        SpawnCount++;
        if (SpawnCount >= EnemiesInWave)
        {
            NextWaveTime = Time.time + TimeBetweenWaves;
        }
        WaypointFollower follower = GameObject.Instantiate(FollowerPrefab, transform.position, Quaternion.identity, null);
        follower.Waypoint = waypoint;
    }

    void StartNewWave()
    {
        SpawnCount = 0;
        NextSpawnTime = Time.time;
    }
}
