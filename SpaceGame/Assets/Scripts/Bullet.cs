using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 1f;

    //private Vector2 direction;

    //void Start(Vector2 direction)
    void Start()
    {
        //this.direction = direction;
    }


    void Update()
    {
        transform.position += new Vector3(0, 1, 0) * Speed * Time.deltaTime;
    }

    private void OnBecameInvisible()
    {
        GameObject.Destroy(gameObject);
    }
}
