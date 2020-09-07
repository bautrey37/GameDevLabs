using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float ForceAmount = 100;
    private Rigidbody2D rigidBody2D;
    private AudioSource audioSource;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidBody2D.velocity = Vector2.zero;
            rigidBody2D.AddForce(new Vector2(0, ForceAmount));
            audioSource.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Restart();
    }

    private void Restart()
    {
        Game.Instance.Restart();
        rigidBody2D.velocity = Vector2.zero;
        transform.position = new Vector3(transform.position.x, 0, 0);
    }

    // restart game when bird falls out of screen
    // there is a function for this. 

}
