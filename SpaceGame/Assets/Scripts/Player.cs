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
    public Shield ShieldPrefab;
    //public Laser LaserPrefab;
    private GameObject laser;

    private float _nextRespawn = 0;
    private bool _dead;

    //private Vector3 _lastPosition;

    private int _score;
    public int Score
    {
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

    private float _laserEnergyCost;
    private float _shieldEnergyCost;
    private float _energyRecharge;
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

    public bool PowerupSplitter;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Score = 0;
        Energy = 1;
        _energyRecharge = 0.002f;
        Lives = 3;
        _dead = false;

        ShieldPrefab = GameObject.Instantiate<Shield>(ShieldPrefab, transform.position, Quaternion.identity, gameObject.transform);
        _shieldEnergyCost = 0.001f;

        //LaserPrefab = GameObject.Instantiate<Laser>(LaserPrefab, transform.position, Quaternion.identity, gameObject.transform);
        laser = gameObject.transform.GetChild(0).gameObject;
        laser.SetActive(false);
        _laserEnergyCost = 0.003f;

        PowerupSplitter = false;

        InvokeRepeating("launchBullet", 0f, BulletDelay);
    }

    private void launchBullet()
    {
        if (!_dead && !ShieldPrefab.isActive() && !laser.activeSelf)
        {
            if (PowerupSplitter)
            {
                // straight up
                GameObject.Instantiate<PlayerBullet>(BulletPrefab, transform.position, Quaternion.identity, null);

                // left shoot
                PlayerBullet leftBullet = GameObject.Instantiate<PlayerBullet>(BulletPrefab, transform.position, Quaternion.identity, null);
                leftBullet.SetMovementAngle(-10);

                // right shoot
                PlayerBullet rightBullet = GameObject.Instantiate<PlayerBullet>(BulletPrefab, transform.position, Quaternion.identity, null);
                rightBullet.SetMovementAngle(10);
            }
            else
            {
                GameObject.Instantiate<PlayerBullet>(BulletPrefab, transform.position, Quaternion.identity, null);
            }
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
        //var direction = transform.position - _lastPosition;
        //Vector2 localDirection = transform.InverseTransformDirection(direction);
        //_lastPosition = transform.position;

        //Debug.Log(localDirection);
        // Comparison of Vector2 is not accurate
        //if (localDirection != Vector2.zero)
        //{
        //    //Debug.Log("Not 0 velocity");
        //    transform.rotation = Quaternion.Euler(0, 0, GetRotation(localDirection));
        //}

        if (!_dead)
        {
            handleShieldControls();
            handleLaserControls();

            if (!ShieldPrefab.isActive() && !laser.activeSelf)
            {
                Energy += _energyRecharge;
            }
        }

        if (_dead && Time.time >= _nextRespawn)
        {
            _dead = false;
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }
    }
/*
    private float GetRotation(Vector2 direction)
    {
        return 0f;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        Debug.Log(angle);
        return angle;
    }*/

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

    // Spawn shield on right button down, do not spawn on no energy
    private void handleShieldControls()
    {
        if (Input.GetMouseButtonDown(1) && Energy != 0 && !laser.activeSelf)
        {
            ShieldPrefab.activate();
        }

        if (Input.GetMouseButtonUp(1))
        {
            ShieldPrefab.deactivate();
        }

        if (Energy == 0)
        {
            ShieldPrefab.deactivate();
        }

        if (ShieldPrefab.isActive())
        {
            Energy -= _shieldEnergyCost;
        }
    }

    // Spawn laser on left button down, do not spawn on no energy
    private void handleLaserControls()
   {
        if (Input.GetMouseButtonDown(0) && Energy != 0 && !ShieldPrefab.isActive())
        {
            laser.SetActive(true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            laser.SetActive(false);
        }

        if (Energy == 0)
        {
            laser.SetActive(false);
        }

        if (laser.activeSelf)
        {
            Energy -= _laserEnergyCost;
            // set end point to be screen end
            laser.GetComponent<LineRenderer>().SetPosition(1, new Vector3(0, Screen.height - Input.mousePosition.y, 0));
            Debug.Log(Screen.height - Input.mousePosition.y);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Hit()
    {
        Debug.Log("Player Hit!");
        if (!_dead)
        {
            Lives--;
            ShieldPrefab.deactivate();
            laser.SetActive(false);
            _dead = true;

            if (Lives == 0)
            {
                gameObject.SetActive(false);
                HUD.Instance.ShowLoseScreen();
            }
            else
            {
                // show invinsibility mode
                GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
                _nextRespawn = Time.time + RespawnDelay;
            }
        }
    }

    public bool IsDead()
    {
        return _dead;
    }
}
