using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerupType
{
    Life,
    ShooterSplitter
}

public class Powerup : MonoBehaviour
{
    public PowerupType powerupType;
    public Vector2 MovementVector;

    private new SpriteRenderer renderer;

    void Start()
    { 
        renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (renderer.isVisible)
        {
            transform.position += (Vector3)MovementVector * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null && !player.IsDead())
        {
            if (powerupType == PowerupType.Life)
            {
                player.Lives++;

            }
            if (powerupType == PowerupType.ShooterSplitter)
            {
                player.PowerupSplitter = true;
            }
            GameObject.Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
