using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class createnavmesh : MonoBehaviour
{
    // Start is called before the first frame update
    
    
    NavMeshSurface navmeshsurface;
    void Start()
    {
        GameObject navmesh = gameObject;
        navmeshsurface = navmesh.AddComponent<NavMeshSurface>();
        navmeshsurface.collectObjects = CollectObjects.Children;
        navmeshsurface.BuildNavMesh();
        Debug.Log("Navmesh generated");
       
    }

    
}
