using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    private int MinX = 0;

    private int MinY = 0;

    private int MaxX = 0;

    private int MayY = 0;

    public int HalfCubeNumber = 2;

    private Color _color1;
    private bool _colorSet1 = false;
    private Color _color2;
    private bool _colorSet2 = false;
    
    List<GameObject> Cubes = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetColor(Color color)
    {
        if (!_colorSet1)
        {
            _color1 = color;
            _colorSet1 = true;
        } else if (!_colorSet2)
        {
            _color2 = color;
            _colorSet2 = true;
            Spawn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Destroy()
    {
        foreach (GameObject item in Cubes)
        {
            Destroy(item);
        }
        Cubes.Clear();
    }
    
    public void Spawn()
    {
        if (_colorSet1 && _colorSet2)
        {
            for (int x = 0; x < 2; x++)
            {
                for (int i = 0; i < HalfCubeNumber; i++)
                {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    Cubes.Add(cube);
                    cube.gameObject.GetComponent<Renderer>().material.SetColor("_Color", x == 0 ? _color1 : _color2);
                }
            }
        }
    }
}
