using System.Collections;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    public Camera cameraToLookAt;

    public void Awake()
    {
        cameraToLookAt = Camera.main;
    }

    void Update()
    {
        Vector3 v = cameraToLookAt.transform.position - transform.position;
        v.x = v.z = 0.0f;
        transform.LookAt(cameraToLookAt.transform.position - v);
    }
}