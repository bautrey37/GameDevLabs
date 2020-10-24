using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerCardPresenter : MonoBehaviour
{
    public TowerData TowerData;

    public TextMeshProUGUI CostText;
    public TextMeshProUGUI ShortcutText;
    public Image IconImage;

    private Button button;
    private KeyCode keyCode;

    public void Awake()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(Pressed);
        }

        if (TowerData != null)
        {
            CostText.text = TowerData.Cost.ToString();
            ShortcutText.text = TowerData.Shortcut;
            IconImage.sprite = TowerData.Icon;
            keyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), ShortcutText.text);
        }

        Events.OnSetGold += OnSetGold;
    }

    private void OnDestroy()
    {
        Events.OnSetGold -= OnSetGold;
    }

    public void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            Pressed();
        }
    }

    public void Pressed()
    {
        Events.SelectTower(TowerData);
    }

    void OnSetGold(int value)
    {
        if (TowerData.Cost > value)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }
}
