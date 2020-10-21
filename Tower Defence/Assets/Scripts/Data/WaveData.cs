using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveData
{
    public EnemyData EnemyType;
    public int NumberOfEnemies = 3;
    public float TimeBetweenSpawns = 1f;
}
