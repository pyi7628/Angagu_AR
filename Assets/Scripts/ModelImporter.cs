using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;

public class ModelImporter : MonoBehaviour
{

    private string modelName = "DesignChair1.dae";
    //public Button download;
    //public ObjImporter objImporter;
    //public GameObject emptyPrefabWithMeshRenderer;

    // Use this for initialization
    void Start()
    {
        Debug.Log("Started");
        StartCoroutine(ImportObject());
    }

    // Update is called once per frame
    IEnumerator ImportObject()
    {

        UnityWebRequest www = new UnityWebRequest("http://d3u3zwu9bmcdht.cloudfront.net/testModel/" + modelName);

        www.downloadHandler = new DownloadHandlerBuffer();
        yield return www.SendWebRequest();
        Debug.Log("http://d3u3zwu9bmcdht.cloudfront.net/testModel/" + modelName);

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        } 
       
        else
        {
            string write_path = Application.dataPath + "/Models/" + modelName;

            byte[] results = www.downloadHandler.data;

            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            ms.Write(results, 0, results.Length);
            ms.Seek(0, SeekOrigin.Begin);
            Object obj = (Object) bf.Deserialize(ms);
            //System.IO.File.WriteAllBytes(write_path, results);

            Debug.Log(write_path);
            Debug.Log(results);
            Object spawnedPrefab;
            
            spawnedPrefab = Instantiate(obj, new Vector3(0.0f,0.0f,0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));

        }



        //Mesh importedMesh = objImporter.ImportFile(Application.dataPath + "/Objects/" + modelName);
      
        //spawnedPrefab.transform.position = new Vector3(0, 0, 0);
        //spawnedPrefab.GetComponent<MeshFilter>().mesh = importedMesh;
    }
}
