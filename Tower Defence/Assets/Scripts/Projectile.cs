using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int Damage = 1;
    public float Speed = 5f;
    public Health Target;

    void Update()
    {
        if (Target == null)
        {
            Destroy();
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, Time.deltaTime * Speed);

        float distance = Vector3.SqrMagnitude(transform.position - Target.transform.position);
        if (distance <= float.Epsilon)
        {
            Target.Hit(Damage);
            Destroy();
        }
    }

    private void Destroy()
    {
        GameObject.Destroy(this.gameObject);
    }
}
