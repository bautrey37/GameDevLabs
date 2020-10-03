using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDPresenter : MonoBehaviour
{
    public TextMeshProUGUI MoneyText;

    private void Awake()
    {
        Events.OnSetMoney += OnSetMoney;
    }

    private void OnDestroy()
    {
        Events.OnSetMoney -= OnSetMoney;
    }

    private void OnSetMoney(int amount)
    {
        MoneyText.text = "Money: " + amount;
    }
}
