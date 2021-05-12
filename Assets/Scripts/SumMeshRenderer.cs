using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SumMeshRenderer : MonoBehaviour
{
    public Bounds bounds;
    public GameObject originModel;

    void OnDrawGizmosSelected()
    {
        Bounds totalBounds = new Bounds();
        foreach (MeshRenderer meshRenderer in GetComponentsInChildren<MeshRenderer>())
        {
            totalBounds.Encapsulate(meshRenderer.bounds);
        }
        Color temp = Color.red;
        temp.a = 0.3f;
        Gizmos.color = temp;
        Gizmos.DrawCube(totalBounds.center, totalBounds.size);
    }
    void resizing(Vector3 realSize)
    {
        Bounds totalBounds = new Bounds();
        foreach (MeshRenderer meshRenderer in GetComponentsInChildren<MeshRenderer>())
        {
            totalBounds.Encapsulate(meshRenderer.bounds);
        }
        Debug.Log(totalBounds.size);

        Vector3 boundSize = totalBounds.size;
        // 일단 한쪽 비율만 보고 일정하게 줄이
        float resizeRate =  realSize.x / boundSize.x;
        transform.localScale = new Vector3(resizeRate, resizeRate, resizeRate);

        originModel.transform.localScale = new Vector3(resizeRate, resizeRate, resizeRate);
        // transform.localScale = new Vector3(1 / (boundSize.x*2), 1 / (boundSize.x * 2), 1 / (boundSize.x * 2));
    }
    void Start()
    {
        // M단위 실제 사이즈 param
        resizing(new Vector3(0.5f, 0.5f, 0.7f));
    }

    void Update()
    {
        
    }
}
