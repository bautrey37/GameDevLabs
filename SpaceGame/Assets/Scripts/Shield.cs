using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    void Start()
    {
        deactivate();
    }

    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Shield collision");
    }

    public void activate()
    {
        gameObject.SetActive(true);
        Debug.Log("Shield active");
    }

    public void deactivate()
    {
        gameObject.SetActive(false);
        Debug.Log("Shield not active");
    }

    public bool isActive()
    {
        return gameObject.activeSelf;
    }
}
