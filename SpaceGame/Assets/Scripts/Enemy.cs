using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** TODO: 
 * rotate towards moving direction
 * 
 */

public class Enemy : MonoBehaviour
{
    public Vector2 MovementVector;
    public float Health = 10;

    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, GetRotation(MovementVector));
        //transform.rotation = Quaternion.AngleAxis(GetRotation(MovementVector), Vector3.back);
    }

    private float GetRotation(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;

        Debug.Log(angle);
        return angle;
    }


    void Update()
    {
        transform.position += (Vector3)MovementVector * Time.deltaTime;
    }

    public void Hit(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Destroy();
        }
    }

    public void Destroy()
    {
        Player.Instance.Score += 1;

        gameObject.SetActive(false);
        //GameObject.Destroy(gameObject);
    }


}
