using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public float BulletDelay = 0.3f; // seconds
    public float RespawnDelay = 1.0f; // seconds
    public PlayerBullet BulletPrefab;

    private float NextRespawn = 0;
    private bool dead;

    private Vector3 lastPosition;

    private int _score;
    public int Score {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
            HUD.Instance.SetScore(_score);
        }
    }

    private float _energy;
    public float Energy
    {
        get
        {
            return _energy;
        }
        set
        {
            _energy = Mathf.Clamp01(value);
            HUD.Instance.SetEnergy(_energy);
        }
    }

    private int _lives;
    public int Lives
    {
        get
        {
            return _lives;
        }
        set
        {
            _lives = Mathf.Clamp(value, 0, 4);
            HUD.Instance.SetLives(_lives);
        }
    }

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Score = 0;
        Energy = 1;
        Lives = 4;
        dead = false;

        InvokeRepeating("launchBullet", 0f, BulletDelay);
    }

    private void launchBullet()
    {
        if (!dead)
        {
            GameObject.Instantiate<PlayerBullet>(BulletPrefab, transform.position, Quaternion.identity, null);
        }
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

        // TODO Extra: make your ship tilting left or right depending on the direction you are moving.
        //Vector2 moveDirection = GetComponent<Rigidbody2D>().velocity;
        var direction = transform.position - lastPosition;
        Vector2 localDirection = transform.InverseTransformDirection(direction);
        lastPosition = transform.position;

        //Debug.Log(localDirection);
        // Comparison of Vector2 is not accurate
        if (localDirection != Vector2.zero)
        {
            //Debug.Log("Not 0 velocity");
            transform.rotation = Quaternion.Euler(0, 0, GetRotation(localDirection));
        } 

        if (dead && Time.time >= NextRespawn)
        {
            dead = false;
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }
        
    }

    private float GetRotation(Vector2 direction)
    {
        return 0f;
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        //Debug.Log(angle);
        //return angle;
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
        if (enemy != null && !dead)
        {
            enemy.Destroy();
            Hit();
        }
        
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Hit()
    {
        if (!dead)
        {
            Lives--;
            if (Lives == 0)
            {
                gameObject.SetActive(false);
                HUD.Instance.ShowLoseScreen();
            }
            else
            {
                // show invinsibility mode
                GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
                dead = true;
                NextRespawn = Time.time + RespawnDelay;
            }
        }
    }
}
