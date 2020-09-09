using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float SpawnDelay = 1;
    public Bullet BulletPrefab;

    private float NextSpawnTime = 0;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;

        Bounds bounds = OrthographicBounds(Camera.main);
        pos = new Vector3(
            Mathf.Clamp(pos.x, bounds.min.x, bounds.max.x),
            Mathf.Clamp(pos.y, bounds.min.y, bounds.max.y),
            0
            );

        transform.position = pos;

        // also use InvokeRepeating method
        if (Time.time >= NextSpawnTime)
        {
            Bullet bullet = GameObject.Instantiate<Bullet>(BulletPrefab, transform.position, Quaternion.identity, null);
            NextSpawnTime += SpawnDelay;
        }
        
    }

    public static Bounds OrthographicBounds(Camera camera)
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = camera.orthographicSize * 2;
        Bounds bounds = new Bounds(
            camera.transform.position,
            new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
        return bounds;
    }
}
