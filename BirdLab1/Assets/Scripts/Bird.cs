using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float ForceAmount = 100;
    private Rigidbody2D _rigidBody2D;
    private AudioSource _audioSource;

    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidBody2D.velocity = Vector2.zero;
            _rigidBody2D.AddForce(new Vector2(0, ForceAmount));
            _audioSource.Play();
        }
        // falls out the bottom
        if (_rigidBody2D.position.y < -6)
        {
            Debug.Log("Fallen out of bottom screen");
            Restart();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Restart();
    }

    private void Restart()
    {
        Game.Instance.Restart();
        _rigidBody2D.velocity = Vector2.zero;
        transform.position = new Vector3(transform.position.x, 0, 0);
    }

    // restart game when bird falls out of screen
    // there is a function for this. 

}
