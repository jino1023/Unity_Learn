using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayer : MonoBehaviour
{
    public GameObject Player;

    public float speed = 20;
    public Vector3 targetLastPosition;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        targetLastPosition = (Player.transform.position - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        // Quaternion targetRotation = Quaternion.LookRotation(Player.transform.position - transform.position);
        // transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
        transform.position += targetLastPosition * speed * Time.deltaTime;
    }
}
