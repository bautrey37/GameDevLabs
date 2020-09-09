using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 1f;

    void Start()
    {

    }


    void Update()
    {
        transform.position += new Vector3(0, 1, 0) * Speed * Time.deltaTime;
    }

    private void OnBecameInvisible()
    {
        GameObject.Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collide");

        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Destroy();
        }
        GameObject.Destroy(gameObject);
    }


}
