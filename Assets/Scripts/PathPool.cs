using System.Collections.Generic;
using UnityEngine;

public class PathPool : MonoBehaviour
{
    public GameObject prefab;

    private const int MAX_OBJECTS = 1024;
    private List<GameObject> objects = new List<GameObject>(MAX_OBJECTS);
    private int activeObjects = 0;


    void Awake()    
    {
        for (int i = 0; i < MAX_OBJECTS; ++i)
        {
            GameObject go = Instantiate(prefab, Vector3.one, Quaternion.identity, this.transform) as GameObject;
            go.SetActive(false);
            objects.Add(go);
        }
    }

    public void ReclaimAll()
    {
        for (int i = 0; i < activeObjects; ++i)
        {
            objects[i].SetActive(false);
        }
        activeObjects = 0;
    }

    public GameObject GiveObject()
    {
        GameObject go = objects[activeObjects++];
        go.SetActive(true);
        return go;
    }

}
