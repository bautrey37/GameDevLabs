using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float Speed = 15f;

    void Start()
    {
    }

    void Update()
    {
        transform.position += new Vector3(0, -1, 0) * Speed * Time.deltaTime;
    }

    private void OnBecameInvisible()
    {
        GameObject.Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            player.Hit();
            GameObject.Destroy(gameObject);
        }
        
    }

    public void Destroy()
    {
        GameObject.Destroy(gameObject);
    }
}
