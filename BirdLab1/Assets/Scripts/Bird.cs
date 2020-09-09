using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float ForceAmount = 100;
    private Rigidbody2D _rigidBody2D;
    private BoxCollider2D _collider2D;

    private AudioSource[] sound;
    private AudioSource jump;
    private AudioSource death;

    private bool dead;

    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<BoxCollider2D>();

        sound = GetComponents<AudioSource>();
        jump = sound[0];
        death = sound[1];

        dead = false;

        Jump();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.timeScale == 1)
        {
            Jump();
        }
    }

    // Hits wall
    private void OnTriggerEnter2D(Collider2D collision)
    {
        death.Play();
        dead = true;
        Game.Instance.StopGame();
        _collider2D.enabled = false;
        _rigidBody2D.velocity = Vector2.zero;
        _rigidBody2D.AddForce(new Vector2(0, ForceAmount));

    }

    // went out of screen
    private void OnBecameInvisible()
    {
        if (!dead) { death.Play(); }
        Restart();
    }

    private void Restart()
    {   
        Game.Instance.Restart();
        _collider2D.enabled = true;

        _rigidBody2D.velocity = Vector2.zero;
        //transform.position = new Vector3(transform.position.x, 0, 0);
        transform.position = new Vector3(-7, 0, 0);

        dead = false;
    }

    private void Jump()
    {
        _rigidBody2D.velocity = Vector2.zero;
        _rigidBody2D.AddForce(new Vector2(0, ForceAmount));
        jump.Play();
    }
}
