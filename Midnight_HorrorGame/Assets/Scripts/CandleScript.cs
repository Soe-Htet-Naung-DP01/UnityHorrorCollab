using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleScript : MonoBehaviour
{
    public Transform ghostPosition;
    public GameObject lightSource; // LightSource
    public float distanceFromGhost = 0;
    public float dangerousDistance = 5f;
    void Start()
    {
        lightSource.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        DetectGhost();
    }

    void DetectGhost()//Again, It does what it sounds like
    {
        distanceFromGhost = Vector3.Distance(this.transform.position, ghostPosition.position); //Find the distance

        if(distanceFromGhost <= dangerousDistance)//if the ghost is close or in dangerZone
        {
            Debug.Log("Chuckles! I'm in DANGERRRRR"); //replace this with what we are going to do to alarm the player that the ghost is near.
            lightSource.SetActive(false);
        }
        else if (distanceFromGhost > dangerousDistance)
        {
            lightSource.SetActive(true);
        }
    }
}
