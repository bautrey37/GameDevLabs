using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoller : MonoBehaviour
{

    public float Speed = 1f;

    void Update()
    {
        transform.position += new Vector3(0, 1, 0) * Speed * Time.deltaTime;

        if (!Player.Instance.IsDead() && gameObject.transform.position.y > 35)
        {
            HUD.Instance.StartLevel(HUD.Instance.CurrentLevel + 1);
        }

        if (!Player.Instance.IsDead() && HUD.Instance.CurrentLevel == 2 && gameObject.transform.position.y >= 34)
        {
            HUD.Instance.ShowWinScreen();
        }
    }
}
