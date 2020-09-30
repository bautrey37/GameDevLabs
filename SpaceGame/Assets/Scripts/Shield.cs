using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    void Start()
    {
        deactivate();
    }

    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyBullet enemyBullet = collision.GetComponent<EnemyBullet>();
        if (enemyBullet != null)
        {
            enemyBullet.Destroy();
        }

        EnemySeekBullet enemySeekBullet = collision.GetComponent<EnemySeekBullet>();
        if (enemySeekBullet != null)
        {
            enemySeekBullet.Destroy();
        }
    }

    public void activate()
    {
        gameObject.SetActive(true);
    }

    public void deactivate()
    {
        gameObject.SetActive(false);
    }

    public bool isActive()
    {
        return gameObject.activeSelf;
    }
}
