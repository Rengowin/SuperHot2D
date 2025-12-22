using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayer : MonoBehaviour
{ 
    [SerializeField]
    GameObject player;

    [SerializeField]
    float minDistanceToPlayer;

    [SerializeField]
    float speed;

    Vector3 currentPosition;
    Vector3 directionTowardsPlayer;
    float distance;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        currentPosition = transform.position;
        if (Vector3.Distance(currentPosition, player.transform.position) > minDistanceToPlayer)
        {
            directionTowardsPlayer = (player.transform.position - currentPosition).normalized;
            distance = Vector3.Distance(currentPosition, player.transform.position);
            transform.position += Vector3.ClampMagnitude(directionTowardsPlayer * speed * Time.deltaTime, distance);
        }
    }
}

