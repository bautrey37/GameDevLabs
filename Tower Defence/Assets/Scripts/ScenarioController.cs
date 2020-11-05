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
    private int timeBetweenWaves = 4;
    private float timeToNextWave;
    private float endGameBuffer = 15;


    private void Awake()
    {
        Events.OnSetGold += OnSetGold;
        Events.OnSetLives += OnSetLives;

        Events.OnRequestGold += OnRequestGold;
        Events.OnRequestLives += OnRequestLives;

        Events.OnStartLevel += OnStartLevel;
        Events.OnEndLevel += OnEndLevel;

        EndGamePanel.SetActive(false);
        EndGameButton.onClick.AddListener(BackToMenuClick);
    }

    private void OnDestroy()
    {
        Events.OnSetGold -= OnSetGold;
        Events.OnSetLives -= OnSetLives;

        Events.OnRequestGold -= OnRequestGold;
        Events.OnRequestLives -= OnRequestLives;

        Events.OnStartLevel -= OnStartLevel;
        Events.OnEndLevel -= OnEndLevel;
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

    private void OnStartLevel(ScenarioData data)
    {
        scenarioData = data;
        levelRunning = true;
        currentWaveIndex = 0;

        Events.SetLives(scenarioData.Lives);
        Events.SetGold(scenarioData.StartingGold);
    }

    private void Update()
    {
        if (levelRunning &&
            Time.time > timeToNextWave &&
            currentWaveIndex < scenarioData.Waves.Length)
        {
            Debug.Log("Starting wave " + currentWaveIndex);
            StartWave();
        }
        checkWinLevel();
    }

    private void StartWave()
    {
        Events.StartWave(scenarioData.Waves[currentWaveIndex]);
        currentWaveIndex++;

        WaveData wave = scenarioData.Waves[currentWaveIndex];
        timeToNextWave = Time.time // current time
            + wave.NumberOfEnemies * wave.TimeBetweenSpawns // time for enemies to spawn
            + timeBetweenWaves; // wave delay
    }

    // when all the enemies of the last wave have passed enough time to get through the level
    // ideally would like when enemies are all killed, but I need to keep track of the enemies
    // which are spawned in the spawner class, which is difficult.
    private void checkWinLevel()
    {
        if (levelRunning &&
            currentWaveIndex == scenarioData.Waves.Length &&
            Time.time > timeToNextWave + endGameBuffer)
        {
            Events.EndLevel(true);
        }
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
}
