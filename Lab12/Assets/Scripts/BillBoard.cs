using System.Collections;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    public ParticleSystem Explosion;

    private Camera cameraToLookAt;

    private bool exploded = false;

    Animator animator;

    public void Awake()
    {
        cameraToLookAt = Camera.main;
    }

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 v = cameraToLookAt.transform.position - transform.position;
        v.x = v.z = 0.0f;
        transform.LookAt(cameraToLookAt.transform.position - v);
    }

    private void OnTriggerEnter(Collider other)
    {
        CharacterController cont = other.GetComponent<CharacterController>();
        if (!exploded && cont)
        {
            animator.SetTrigger("Explode");
            Explode();
        }
        //exploded = true;
    }

    public void Explode()
    {
        Explosion.Play();
    }
}