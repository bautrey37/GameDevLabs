﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupLife : MonoBehaviour
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
        if (player != null && !player.isDead())
        {
            player.Lives++;
            GameObject.Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
