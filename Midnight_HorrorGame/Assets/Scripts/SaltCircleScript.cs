using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SaltCircleScript : MonoBehaviour
{
    // how long in seconds the circle will last
    private float circleDuration = 10.0f;

    // how close the circle can be to the midnight man before it gets destroyed immediately
    public float minDistance = 2.0f;

    // used to help rebake the mesh
    public GameObject midnightMan;

    // Start is called before the first frame update
    void Start()
    {
        // Destroy circle after a predetermined seconds
        Invoke(nameof(DestroyCircle), circleDuration);

        // finds the midnight man game object
        midnightMan = GameObject.FindGameObjectWithTag("Ghost");

        // code for rebaking the navmesh
        midnightMan.GetComponent<MnmGhostNavMesh>().RefreshNavMesh();
    }

    void Update()
    {
        if (IsTooClose())
        {
            DestroyCircle();
        }
    }

    void DestroyCircle()
    {
        GetComponent<NavMeshModifier>().overrideArea = false;
        midnightMan.GetComponent<MnmGhostNavMesh>().RefreshNavMesh();
        Destroy(gameObject);
    }

    bool IsTooClose()
    {
        if (Vector3.Distance(midnightMan.transform.position, transform.position) >= minDistance)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

}
