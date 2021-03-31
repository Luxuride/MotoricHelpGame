using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class EggSpawner : MonoBehaviour
{
    public GameObject eggPrefab;

    // Start is called before the first frame update
    void Start()
    {
        CreateEgg();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateEgg()
    {
        Instantiate(eggPrefab, new Vector3(-0.2f, 2f, -2.7f), Quaternion.identity);
    }
}
