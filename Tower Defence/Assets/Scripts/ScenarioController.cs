using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ScenarioController : MonoBehaviour
{
    public TextMeshProUGUI GoldText;
    public TextMeshProUGUI LivesText;

    public GameObject EndGamePanel;
    public TextMeshProUGUI EndGameText;
    public Button EndGameButton;
    public ScenarioData DefaultScenarioData;

    private int gold;
    private int lives;

    private bool levelRunning;
    private ScenarioData scenarioData;
    private int currentWaveIndex;
    
    private void Awake()
    {
        Events.OnSetGold += OnSetGold;
        Events.OnSetLives += OnSetLives;

        Events.OnRequestGold += OnRequestGold;
        Events.OnRequestLives += OnRequestLives;

        Events.OnStartLevel += OnStartLevel;
        Events.OnEndLevel += OnEndLevel;

        Events.OnEndWave += WaveCompleted;

        EndGamePanel.SetActive(false);
        EndGameButton.onClick.AddListener(BackToMenuClick);
    }

    public void Start()
    {
        // only used when scene is started not from Menu
        if (scenarioData == null)
        {
            scenarioData = DefaultScenarioData;
        }
        Events.StartLevel(scenarioData);
    }

    private void OnDestroy()
    {
        Events.OnSetGold -= OnSetGold;
        Events.OnSetLives -= OnSetLives;

        Events.OnRequestGold -= OnRequestGold;
        Events.OnRequestLives -= OnRequestLives;

        Events.OnStartLevel -= OnStartLevel;
        Events.OnEndLevel -= OnEndLevel;

        Events.OnEndWave -= WaveCompleted;
    }

    private void OnStartLevel(ScenarioData data)
    {
        scenarioData = data;
       
        Events.SetLives(scenarioData.Lives);
        Events.SetGold(scenarioData.StartingGold);

        currentWaveIndex = 0;
        Events.StartWave(scenarioData.Waves[currentWaveIndex]);

        levelRunning = true;
    }

    private void OnEndLevel(bool isWin)
    {
        if (!levelRunning) return;
        levelRunning = false;

        EndGamePanel.SetActive(true);
        if (isWin)
        {
            EndGameText.text = "VICTORY!";
        }
        else
        {
            EndGameText.text = "GAME LOST!";
        }
    }

    private void BackToMenuClick()
    {
        MenuPresenter.Instance?.gameObject.SetActive(true);
        SceneManager.LoadScene(0);
    }

    // when all the enemies of the waves has been killed
    private void checkWinLevel()
    {
        if (currentWaveIndex == scenarioData.Waves.Length && levelRunning)
        {
            Events.EndLevel(true);
        }
    }

    private void OnSetGold(int amount)
    {
        gold = amount;

        GoldText.text = "Gold: " + amount;
    }    

    private void OnSetLives(int amount)
    {
        if (lives > 0 && amount <= 0)
        {
            Events.EndLevel(false);
        }
        lives = Mathf.Max(0, amount);

        LivesText.text = "Lives: " + lives;
    }

    private int OnRequestGold() => gold;
    private int OnRequestLives() => lives;

    private void WaveCompleted()
    {
        if (currentWaveIndex < scenarioData.Waves.Length)
        {
            currentWaveIndex++;
            Events.StartWave(scenarioData.Waves[currentWaveIndex]);
        }
        checkWinLevel();
    }


}
