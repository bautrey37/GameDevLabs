using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private List<Health> EnemiesInRange;

    private void Awake()
    {
        EnemiesInRange = new List<Health>();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health enemy = collision.GetComponent<Health>();
        if (enemy != null)
        {
            Debug.Log("Enemy Added");
            EnemiesInRange.Add(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Health enemy = collision.GetComponent<Health>();
        if (enemy != null)
        {
            Debug.Log("Enemy Removed");
            EnemiesInRange.Remove(enemy);
        }
    }


}
