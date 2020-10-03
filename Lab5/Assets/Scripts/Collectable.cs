using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int ScoreIncrease = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Events.SetMoney(Events.RequestMoney() + ScoreIncrease);

        GameObject.Destroy(gameObject);
    }


}
