using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public EnemyBullet BulletPrefab;
    public EnemySeekBullet BulletSeekPrefab;
    public float BulletDelay = 0.3f; // seconds
    public bool IsBulletSeek = false;

    private void OnBecameVisible()
    {
        InvokeRepeating("launchBullet", 0f, BulletDelay);
    }

    private void launchBullet()
    {
        if (gameObject.activeSelf)
        {
            if (IsBulletSeek)
            {
                GameObject.Instantiate<EnemySeekBullet>(BulletSeekPrefab, transform.position, Quaternion.identity, null);
            }
            else
            {
                GameObject.Instantiate<EnemyBullet>(BulletPrefab, transform.position, Quaternion.identity, null);
            }

        }
    }
}
