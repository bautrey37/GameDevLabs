using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public static HUD Instance;

    public GameObject LivesPanel;
    public Image EnergyBar;
    public Text ScoreText;

    public void Awake()
    {
        Instance = this;
    }

    public void SetScore(int score)
    {
        ScoreText.text = "Score: " + score;
    }
}
