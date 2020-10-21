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

    private int Gold;
    private int Lives;

    private bool levelRunning;
    private ScenarioData scenarioData;
    
    private void Awake()
    {
        Events.OnSetGold += OnSetGold;
        Events.OnSetLives += OnSetLives;

        Events.OnRequestGold += OnRequestGold;
        Events.OnRequestLives += OnRequestLives;

        EndGamePanel.SetActive(false);
        EndGameButton.onClick.AddListener(BackToMenuClick);
    }

    public void Start()
    {

        OnStartLevel();
    }

    private void OnDestroy()
    {
        Events.OnSetGold -= OnSetGold;
        Events.OnRequestGold -= OnRequestGold;
    }

    private void OnStartLevel()
    {
        // add spawn delay
        // spawn enemies
        //Events.SetLives(scenarioData.Lives);
        //Events.SetGold(scenarioData.StartingGold);

        levelRunning = true;
    }

    private void OnEndLevel(bool value)
    {
        levelRunning = false;

        EndGamePanel.SetActive(true);
        if (value)
        {
            EndGameText.text = "VICTORY";
        }
        else
        {
            EndGameText.text = "LOST";
        }
    }

    private void BackToMenuClick()
    {
        MenuPresenter.Instance?.gameObject.SetActive(true);
        SceneManager.LoadScene(0);
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

    

    private void OnSetLives(int amount)
    {
        Lives = amount;
    }

    private int OnRequestGold() => Gold;
    private int OnRequestLives() => Lives;
}
