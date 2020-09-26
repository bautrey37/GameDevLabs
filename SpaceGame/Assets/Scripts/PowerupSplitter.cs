using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSplitter : MonoBehaviour
{
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
            player.PowerupSplitter = true;
            GameObject.Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
