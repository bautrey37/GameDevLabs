using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Vector2 MovementVector;
    public float Health = 10;
    public EnemyBullet BulletPrefab;
    public float BulletDelay = 0.3f; // seconds

    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, GetRotation(MovementVector));
    }

    private float GetRotation(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        return angle;
    }

    private void OnBecameVisible()
    {
        InvokeRepeating("launchBullet", 0f, BulletDelay);
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    private void launchBullet()
    {
        if (gameObject.activeSelf)
        {
            GameObject.Instantiate<EnemyBullet>(BulletPrefab, transform.position, Quaternion.identity, null);
        }
    }


    void Update()
    {
        transform.position += (Vector3)MovementVector * Time.deltaTime;
    }

    public void Hit(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Destroy();
        }
    }

    public void Destroy()
    {
        Player.Instance.Score += 1;

        gameObject.SetActive(false);
        //GameObject.Destroy(gameObject);
    }

}
