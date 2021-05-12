using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableMeshRenderer : MonoBehaviour
{
    public MeshRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(renderer.bounds.size);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
