using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class StoneSpawner : MonoBehaviour
{
    public GameObject stonePrefab;

    // Start is called before the first frame update
    void Start()
    {
        CreateStone();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateStone()
    {
        Instantiate(stonePrefab, new Vector3(-0.2f, 2f, -2.7f), Quaternion.identity);
    }
}
