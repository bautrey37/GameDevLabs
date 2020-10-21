using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TowerDefence/Scenario")]
public class ScenarioData : ScriptableObject
{
    public string PresentedName;
    public string SceneName;
    public int Lives = 5;
    public int StartingGold = 5;

    public WaveData[] Waves;
}
