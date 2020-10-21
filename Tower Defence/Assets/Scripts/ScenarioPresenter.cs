using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScenarioPresenter : MonoBehaviour
{
    public TextMeshProUGUI NameText;

    private ScenarioData ScenarioData;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void SetData(ScenarioData data)
    {
        ScenarioData = data;
        NameText.text = data.PresentedName;

    }

    private void OnClick()
    {

    }

}
