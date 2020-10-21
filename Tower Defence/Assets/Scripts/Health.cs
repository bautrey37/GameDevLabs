using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public void Hit(int damage)
    {
        // TODO: add gold when enemy destroyed
        GameObject.Destroy(this.gameObject);
    }
}
