using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleScript : MonoBehaviour
{
    public Transform ghostPosition;
    public float distanceFromGhost = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckForGhost();
    }

    void CheckForGhost()
    {
        distanceFromGhost = Vector3.Distance(this.transform.position, ghostPosition.position);
    }
}
