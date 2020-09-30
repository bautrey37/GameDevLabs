using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float Speed = 10f;

    private float _hitPower = 3;

    public void SetMovementAngle(float angle)
    {   
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void Update()
    {
        transform.position += transform.rotation * transform.up * Speed * Time.deltaTime;
    }

    private void OnBecameInvisible()
    {
        GameObject.Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
  
        if (enemy != null)
        {
            enemy.Hit(_hitPower);
            GameObject.Destroy(gameObject);
        }
    }
}
