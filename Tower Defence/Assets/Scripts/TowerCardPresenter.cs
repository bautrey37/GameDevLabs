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
        }
    }

    public void Pressed()
    {
        Events.SelectTower(TowerData);
    }
}
