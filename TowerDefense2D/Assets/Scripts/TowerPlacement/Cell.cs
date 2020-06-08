using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public GridChunk chunk;
    public int index;
    MeshCollider meshCollider;
    

    /// <summary>
    /// Assigns a mesh to the mesh collider
    /// </summary>
    /// <param name="mesh">Mesh mesh of the cell </param>
    public void AssignCollider(Mesh mesh) {

        meshCollider = gameObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;
    }

   
}
