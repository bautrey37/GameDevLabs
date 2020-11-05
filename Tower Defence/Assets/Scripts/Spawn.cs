using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public WaypointFollower FollowerPrefab;
    public float GameStartDelay = 2f;

    private float NextSpawnTime = 0;
    private int SpawnCount;

    private Waypoint waypoint;

    // wave data
    private WaveData waveData;
    private int EnemiesInWave;
    private float TimeBetweenSpawn;

    // enemy data
    private EnemyData enemyData;
    private Sprite enemySprite;
    private int enemyHealth;
    private float enemySpeed;


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
    }

    void SpawnEnemy()
    {
        NextSpawnTime = Time.time + TimeBetweenSpawn;
        SpawnCount++;

        WaypointFollower follower = GameObject.Instantiate(FollowerPrefab, transform.position, Quaternion.identity, null);
        follower.Waypoint = waypoint;
        follower.Speed = enemySpeed;
        follower.GetComponent<SpriteRenderer>().sprite = enemySprite;
        follower.GetComponent<Health>().HealthAmount = enemyHealth;
    }

    public void StartNewWave(WaveData wave)
    {
        waveData = wave;
        EnemiesInWave = waveData.NumberOfEnemies;
        TimeBetweenSpawn = waveData.TimeBetweenSpawns;

        enemyData = waveData.EnemyType;
        enemySprite = enemyData.Sprite;
        enemyHealth = enemyData.Health;
        enemySpeed = enemyData.MovementSpeed;

        SpawnCount = 0;
        NextSpawnTime = Time.time;
    }
}
