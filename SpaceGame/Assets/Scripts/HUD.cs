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
    public TextMeshProUGUI endGameText;
    public Button RestartButon;

    public void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        EndGamePanel.SetActive(false);
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
        //endGameText.text = "You Won!";
    }

    public void ShowLoseScreen()
    {
        endGameText.text = "You Lost!";
        EndGamePanel.SetActive(true);
    }

    public void Restart()
    {
        EndGamePanel.SetActive(false);
        Player.Instance.Restart();
    }
}
