using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int GoldAdd = 1;

    public void Hit(int damage)
    {
        Events.SetGold(Events.RequestGold() + GoldAdd);
        GameObject.Destroy(this.gameObject);
    }
}
