using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public WaypointFollower FollowerPrefab;
    public float TimeBetweenSpawn = 0.5f;
    public float TimeBetweenWaves = 2;
    public int EnemiesInWave = 5;
    public float GameStartDelay = 2f;

    private float NextSpawnTime = 0;
    private float NextWaveTime = 0;
    private int SpawnCount;

    private Waypoint waypoint;
    private WaveData waveData;

    void Awake()
    {
        waypoint = GetComponent<Waypoint>();

        Events.OnStartWave += StartNewWave;
    }

    private void OnDestroy()
    {
        Events.OnStartWave -= StartNewWave;
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
        //else
        //{
        //    if (Time.time > NextWaveTime)
        //    {
        //        StartNewWave();
        //    }
        //}
        
    }

    void SpawnEnemy()
    {
        NextSpawnTime = Time.time + TimeBetweenSpawn;
        SpawnCount++;
        if (SpawnCount >= EnemiesInWave)
        {
            NextWaveTime = Time.time + TimeBetweenWaves;
            // call waveCompleted method 
        }
        WaypointFollower follower = GameObject.Instantiate(FollowerPrefab, transform.position, Quaternion.identity, null);
        follower.Waypoint = waypoint;
    }

    public void StartNewWave(WaveData wave)
    {
        waveData = wave;
        EnemiesInWave = waveData.NumberOfEnemies;
        TimeBetweenSpawn = waveData.TimeBetweenSpawns;
        EnemyData enemyType = waveData.EnemyType;

        SpawnCount = 0;
        NextSpawnTime = Time.time;
    }
}
