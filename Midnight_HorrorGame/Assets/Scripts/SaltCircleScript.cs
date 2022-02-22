using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltCircleScript : MonoBehaviour
{
    // how long in seconds the circle will last
    private float circleDuration = 10.0f;

    // how close the circle can be to the midnight man before it gets destroyed immediately
    public float minDistance = 2.0f;

    public GameObject midnightMan;

    // Start is called before the first frame update
    void Start()
    {
        // Destroy circle after a predetermined seconds
        Invoke(nameof(DestroyCircle), circleDuration);
        // finds the midnight man game object
        midnightMan = GameObject.FindGameObjectWithTag("Ghost");
    }

    void Update()
    {
        if (isTooClose())
        {
            DestroyCircle();
        }
    }

    void DestroyCircle()
    {
        Destroy(gameObject);
    }

    bool isTooClose()
    {
        if (Vector3.Distance(midnightMan.transform.position, this.transform.position) >= minDistance)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
