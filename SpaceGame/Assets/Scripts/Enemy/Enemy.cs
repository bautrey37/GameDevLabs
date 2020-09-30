using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float MoveSpeed = 1;
    public Vector2 MovementVector;
    public float Health = 10;
    public int PointWorth = 2;

    public bool IsWaveMovement = false;
    private float frequency = 5.0f;  // Speed of sine movement
    private float magnitude = 5f;   // Size of sine movement
    private Vector3 axis;

    public GameObject ParticlePrefab;

    private new SpriteRenderer renderer;

    private Vector3 pos;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        pos = transform.position;
        axis = transform.right;
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (renderer.isVisible)
        {
            if(IsWaveMovement)
            {
                pos += (Vector3)MovementVector * Time.deltaTime * MoveSpeed;
                transform.position = pos + axis * Mathf.Sin(Time.time * frequency) * magnitude;
                // TODO rotation??
            }
            else
            {
                transform.position += (Vector3)MovementVector * Time.deltaTime;
                transform.rotation = Quaternion.Euler(0, 0, GetRotation(MovementVector));
            }
        }
    }

    private float GetRotation(Vector2 direction)
    { 
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        return angle;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            player.Hit();
            Destroy();
        }

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
        Instantiate(ParticlePrefab, transform.position, Quaternion.identity, null);

        Player.Instance.Score += PointWorth;

        gameObject.SetActive(false);
    }

}
