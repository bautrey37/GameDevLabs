using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int HealthAmount = 1;
    public int GoldAdd = 1;

    public void Hit(int damage)
    {
        HealthAmount -= damage;
        if (HealthAmount <= 0)
        {
            Events.SetGold(Events.RequestGold() + GoldAdd);
            GameObject.Destroy(this.gameObject);
        }
    }
}
