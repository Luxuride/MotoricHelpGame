using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableScript : MonoBehaviour
{
    public Color color;

    public CubeSpawner cubeSpawner;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Renderer>().material.SetColor("_Color", color);
        cubeSpawner.SetColor(color);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
