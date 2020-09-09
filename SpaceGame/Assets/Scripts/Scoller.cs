using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoller : MonoBehaviour
{

    public float Speed = 1f;

    void Start()
    {
        
    }


    void Update()
    {
        transform.position += new Vector3(0, 1, 0) * Speed * Time.deltaTime;
    }
}
