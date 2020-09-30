using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public static GameControl Instance;

    public void StartGame()
    {
        StartLevel(1);
    }

    public void StartLevel(int level)
    {
        SceneManager.LoadScene(level - 1);
        HUD.Instance.ShowLevelScreen(level);
    }
}
