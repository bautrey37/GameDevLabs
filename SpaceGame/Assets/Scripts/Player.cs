using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float SpawnDelay = 1;
    public Bullet BulletPrefab;
    public int Lives = 3;
    public Vector2 InitLocation;

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

        // rotate the spaceship towards moving direction
        //Vector2 moveDirection = GetComponent<Rigidbody2D>().velocity;
        //if (moveDirection != Vector2.zero)
        //{
        //    float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        //    transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
        //}

        // can also use InvokeRepeating method
        if (Time.time >= NextSpawnTime)
        {
            Bullet bullet = GameObject.Instantiate<Bullet>(BulletPrefab, transform.position, Quaternion.identity, null);
            NextSpawnTime += SpawnDelay;
        }
        
    }

    // https://answers.unity.com/questions/501893/calculating-2d-camera-bounds.html
    public static Bounds OrthographicBounds(Camera camera)
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = camera.orthographicSize * 2;
        Bounds bounds = new Bounds(
            camera.transform.position,
            new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
        return bounds;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Destroy();
        }
        if (Lives == 1)
        {
            RestartGame();
        }
        Lives--;
        transform.position = InitLocation;
        //Input.mousePosition = InitLocation;
    }

    private void RestartGame()
    {
        //SceneManager.LoadScene(0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
