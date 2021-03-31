using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectGroundCollision : MonoBehaviour
{
    public EggSpawner eggSpawner;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name.StartsWith("EggCollider"))
        {
            eggSpawner.CreateEgg();
        }
    }
}
