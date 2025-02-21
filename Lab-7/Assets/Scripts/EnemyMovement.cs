using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public GameObject enemy;
    void Start()
    {

    }
    void Update()
    {
        Vector3 directionToTarget = player.position - enemy.transform.position;
        enemy.transform.Translate(directionToTarget.normalized * Time.deltaTime * 4); // Move toward target
    }
}