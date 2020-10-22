using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float ShootDelay = 0.4f;
    public Projectile ProjectilePrefab;

    private List<Health> EnemiesInRange;

    private float nextShootTime;

    private void Awake()
    {
        EnemiesInRange = new List<Health>();
        nextShootTime = Time.time + ShootDelay;
    }

    private void Update()
    {
        if (nextShootTime <= Time.time)
        {
            Shoot();
            nextShootTime += ShootDelay;
        }
    }

    private void Shoot()
    {
        Health enemy = null;
        while (EnemiesInRange.Count > 0)
        {
            if (EnemiesInRange[0] == null)
            {
                EnemiesInRange.RemoveAt(0);
            }
            else
            {
                enemy = EnemiesInRange[0];
                break;
            }
        }
        if (enemy == null) return;

        Projectile projectile = GameObject.Instantiate(ProjectilePrefab, this.transform.position, Quaternion.identity, null);
        projectile.Target = enemy;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health enemy = collision.GetComponent<Health>();
        if (enemy != null)
        {
            EnemiesInRange.Add(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Health enemy = collision.GetComponent<Health>();
        if (enemy != null)
        {
            EnemiesInRange.Remove(enemy);
        }
    }
}
