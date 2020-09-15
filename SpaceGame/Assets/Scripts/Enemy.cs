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

    }


    void Update()
    {
        transform.position += (Vector3)MovementVector * Time.deltaTime;


    }

    public void Hit(float damage)
    {

    }

    public void Destroy()
    {
        HUD.Instance.SetScore(10);
        gameObject.SetActive(false);
        //GameObject.Destroy(gameObject);
    }


}
