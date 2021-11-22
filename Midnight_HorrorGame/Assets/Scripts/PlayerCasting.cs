using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCasting : MonoBehaviour
{
    public static float DistanceFromTarget;
    public float ToTarget;

    public GameObject player;

    public Vector3 playerPosition;

    void Update()
    {
        RaycastHit Hit;

        playerPosition = player.GetComponent<Transform>().position;



        // outputs how far the object the player is looking at
        if (Physics.Raycast(playerPosition, transform.TransformDirection(Vector3.forward), out Hit))
        {
            ToTarget = Hit.distance;
            DistanceFromTarget = ToTarget;
        }
    }
}
