using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBeaviour : MonoBehaviour
{
    private Transform target;
    private float speed = 15.0f;
    private bool homing;

    private float rocketStrengh = 30.0f;
    private float aliveTimer = 5.0f;

    public void Fire(Transform homingTarget)
    {
        target = homingTarget;
        homing = true;
        Destroy(gameObject, aliveTimer);
    }

    // Moving and Rotating the missile towards the target.
    void Update()
    {
        if (homing && target != null)
        {
            Vector3 moveDirection = (target.transform.position - transform.position).normalized;
            transform.position += moveDirection * speed * Time.deltaTime;
            transform.LookAt(target);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (target != null)
        {
            if (collision.gameObject.CompareTag(target.tag))
            {
                Rigidbody targetRb = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 away = collision.contacts[0].normal;
                targetRb.AddForce(away * rocketStrengh, ForceMode.Impulse);
                Destroy(gameObject);
            }
        }
    }
}
