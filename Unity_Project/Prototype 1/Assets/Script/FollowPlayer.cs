using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private Vector3 offset = new Vector3(0, 6, -8);


    // Update is called once per frame
    void LateUpdate()
    {
        // offset the camera
        transform.position = player.transform.position + offset;
    }
}
