using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScenarioController : MonoBehaviour
{
    public TextMeshProUGUI GoldText;
    public TextMeshProUGUI LivesText;

    private int Gold = 40;
    private int Lives = 3;

    private bool isLevelRunning;
    
    private void Awake()
    {
        Events.OnSetGold += OnSetGold;
        Events.OnRequestGold += OnRequestGold;

        Events.OnSetLives += OnSetLives;
        Events.OnRequestLives += OnRequestLives;
    }

    public void Start()
    {
        Events.SetGold(Gold);

        StartLevel();
    }

    private void OnDestroy()
    {
        Events.OnSetGold -= OnSetGold;
        Events.OnRequestGold -= OnRequestGold;
    }

    private void StartLevel()
    {
        // add spawn delay
        // spawn enemies
    }

    private void WinLevel()
    {
        // when X # of enemies are killed?
    }

    private void LoseLevel()
    {
        // when lives on castle goes to 0
    }

    private void OnSetGold(int amount)
    {
        Gold = amount;
    }

    private int OnRequestGold()
    {
        return Gold;
    }

    private void OnSetLives(int amount)
    {
        Lives = amount;
    }

    private int OnRequestLives()
    {
        return Lives;
    }
}
