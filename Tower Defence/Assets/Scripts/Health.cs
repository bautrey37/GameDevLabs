using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public void Hit(int damage)
    {
        GameObject.Destroy(this.gameObject);
    }
}
