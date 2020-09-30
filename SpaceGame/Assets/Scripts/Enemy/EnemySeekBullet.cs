using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySeekBullet : MonoBehaviour
{
    public float Speed = 15f;
    private Vector3 direction;

    void Start()
    {
        Vector3 playerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = playerPos - transform.position;
        direction.z = 0;
        direction = Vector3.Normalize(direction);
    }

    void Update()
    {
        transform.position += direction * Speed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, GetRotation(direction));
    }

    private float GetRotation(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        return angle;
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
