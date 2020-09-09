using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float ForceAmount = 100;
    private Rigidbody2D _rigidBody2D;

    private AudioSource[] sound;
    private AudioSource jump;
    private AudioSource death;

    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();

        sound = GetComponents<AudioSource>();
        jump = sound[0];
        death = sound[1];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidBody2D.velocity = Vector2.zero;
            _rigidBody2D.AddForce(new Vector2(0, ForceAmount));
            jump.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Restart();
    }

    private void Restart()
    {
        death.Play();
        Game.Instance.Restart();
        _rigidBody2D.velocity = Vector2.zero;
        transform.position = new Vector3(transform.position.x, 0, 0);
    }

    private void OnBecameInvisible()
    {
        Restart();
    }

}
