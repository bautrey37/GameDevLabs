using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public GameObject treeBottom;
    public GameObject TreeTop;

    private void Start()
    {
        Color color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);
        treeBottom.GetComponent<SpriteRenderer>().material.color = color;
        TreeTop.GetComponent<SpriteRenderer>().material.color = color;
    }
}
