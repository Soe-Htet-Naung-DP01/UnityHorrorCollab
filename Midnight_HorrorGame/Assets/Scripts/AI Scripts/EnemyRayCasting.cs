using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRayCasting : MonoBehaviour
{
    // pretty useless rn, no need for the enemy to turn towards player yet

    public static float DistanceFromTarget;
    public float ToTarget;

    public GameObject enemy;

    public Vector3 enemyPosition;

    void Update()
    {
        RaycastHit Hit;

        enemyPosition = enemy.GetComponent<Transform>().position;

        // outputs how far the object the player is looking at
        if (Physics.Raycast(enemyPosition, transform.TransformDirection(Vector3.forward), out Hit))
        {
            ToTarget = Hit.distance;
            DistanceFromTarget = ToTarget;
        }
    }
}
