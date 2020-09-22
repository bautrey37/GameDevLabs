using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    void Start()
    {
        deactivate();
    }

    void Update()
    {
        // somehow stretch laser to end of screen
        //transform.localScale.y =  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Hit(10);
        }
    }

    public void activate()
    {
        gameObject.SetActive(true);
    }

    public void deactivate()
    {
        gameObject.SetActive(false);
    }

    public bool isActive()
    {
        return gameObject.activeSelf;
    }
}
