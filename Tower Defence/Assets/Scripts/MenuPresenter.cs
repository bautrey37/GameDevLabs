using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPresenter : MonoBehaviour
{
    public static MenuPresenter Instance;

    public RectTransform PanelTransform;
    public ScenarioPresenter ScenarioPresenterPrefab;
    public Button ExitButton;

    public List<ScenarioData> Scenarios;
    private ScenarioData SelectedScenario;

    private void Awake()
    {
        if (Instance != null)
        {
            GameObject.Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        Instance = this;

        ExitButton.onClick.AddListener(OnExit);
    }

    private void Start()
    {
        foreach (ScenarioData data in Scenarios)
        {
            ScenarioPresenter presenter = GameObject.Instantiate(ScenarioPresenterPrefab, PanelTransform);
            presenter.SetData(data);
        }
    }

    public void ScenarioSelected(ScenarioData data)
    {
        SelectedScenario = data;
        SceneManager.LoadScene(data.SceneName);

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!scene.name.Equals("MenuScene"))
        {
            gameObject.SetActive(false);
            Events.StartLevel(SelectedScenario);
        }
    }

    private void OnExit()
    {
        Application.Quit();
    }
}
