using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MnmGhostNavMesh : MonoBehaviour
{
    public NavMeshSurface surface;

    public void RefreshNavMesh()
    {
        surface.BuildNavMesh();
    }
}
