using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour
{
    public float SecondsToDestroy = 5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyThisAfterTime(SecondsToDestroy));
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator DestroyThisAfterTime(float destroyTime)
    {
        this.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
        yield return new WaitForSeconds(destroyTime/3);
        this.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
        yield return new WaitForSeconds(destroyTime/3);
        this.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        yield return new WaitForSeconds(destroyTime/3);
        Destroy(this.gameObject);
    }
}
