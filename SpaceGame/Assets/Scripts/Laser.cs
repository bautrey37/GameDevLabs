using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    //LineRenderer laserLine;

    void Start()
    {
        deactivate();
        //laserLine = GetComponent<LineRenderer>();
        //laserLine.startWidth = 0.2f;
        //laserLine.endWidth = 0.2f;

    }

    void Update()
    {
        //laserLine.SetPosition(0, startPoint.position);
        //laserLine.SetPosition(1, endPoint.position);
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
