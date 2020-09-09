using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Vector2 MovementVector;

    void Start()
    {

    }


    void Update()
    {
        transform.position += (Vector3)MovementVector * Time.deltaTime;
    }

    public void Destroy()
    {
        GameObject.Destroy(gameObject);
        HUD.Instance.SetScore(10);
    }
}
