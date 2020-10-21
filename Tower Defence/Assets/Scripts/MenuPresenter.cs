using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MenuPresenter : MonoBehaviour
{
    public RectTransform PanelTransform;
    public ScenarioPresenter ScenarioPresenterPrefab;
    public Button ExitButton;

    public List<ScenarioData> Scenarios;

    private void Awake()
    {
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

    private void OnExit()
    {
        Application.Quit();
    }
}
