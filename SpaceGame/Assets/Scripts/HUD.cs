using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    public static HUD Instance;

    public GameObject LifePrefab;

    public GameObject LivesPanel;
    public Image EnergyBar;
    public Text ScoreText;

    public GameObject EndGamePanel;
    public TextMeshProUGUI EndGameText;
    public Button RestartButon;

    public GameObject LevelPanel;
    public TextMeshProUGUI LevelText;
    private float levelShowTime = 2; //seconds
    public int CurrentLevel;

    public void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        EndGamePanel.SetActive(false);
        ShowLevelScreen(CurrentLevel);
    }

    public void SetScore(int score)
    {
        ScoreText.text = "Score: " + score;
    }

    public void SetEnergy(float value)
    {
        EnergyBar.fillAmount = value;
    }

    public void SetLives(int value)
    {
       for (int i=0; i < LivesPanel.transform.childCount; i++)
       {
            GameObject.Destroy(LivesPanel.transform.GetChild(i).gameObject);
       }

        for (int i = 0; i < value; i++)
        {
            GameObject.Instantiate(LifePrefab, LivesPanel.transform);
        }
    }

    public void ShowWinScreen()
    {
        EndGameText.text = "You Won!";
        EndGamePanel.SetActive(true);
    }

    public void ShowLoseScreen()
    {
        EndGameText.text = "You Lost!";
        EndGamePanel.SetActive(true);
    }

    public void ShowLevelScreen(int level)
    {
        LevelText.text = string.Format("Level {0}", level);
        LevelPanel.SetActive(true);
        StartCoroutine(deactivateLevelPanel());
    }

    private IEnumerator deactivateLevelPanel()
    {
        yield return new WaitForSeconds(levelShowTime);
        LevelPanel.SetActive(false);
    }

    public void Restart()
    {
        EndGamePanel.SetActive(false);
        StartLevel(1);
    }

    public void StartLevel(int level)
    {
        SceneManager.LoadScene(level - 1);
        ShowLevelScreen(level);
    }
}
