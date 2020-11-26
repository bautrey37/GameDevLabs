using System.Collections;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    public ParticleSystem Explosion;

    private Camera cameraToLookAt;
    private Animator animator;

    public void Awake()
    {
        cameraToLookAt = Camera.main;
        Explosion.Stop();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
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
        if (cont)
        {
            animator.SetTrigger("Explode");
        }
    }

    public void Explode()
    {
        Explosion.Play();
    }
}