using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum CubeType
{
    Normal,
    Good,
    Bad
} 
public class CubeScript : MonoBehaviour
{
    public delegate void GoodButtonTouchAction();
    public delegate void BadButtonTouchAction();

    public static event GoodButtonTouchAction OnGoodButtonTouch;
    public static event BadButtonTouchAction OnBadButtonTouch;
    
    private CubeType _cubeType = CubeType.Normal;
    private Coroutine _activeCoroutine;
    
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
        Debug.Log((OnGoodButtonTouch is null).ToString());
    }

    private void OnEnable()
    {
        OnGoodButtonTouch += ResetCube;
        OnBadButtonTouch += ResetCube;
    }

    private void OnDisable()
    {
        
        OnGoodButtonTouch -= ResetCube;
        OnBadButtonTouch -= ResetCube;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public bool BecomeGood(int timeForCube)
    {
        if (_cubeType != CubeType.Normal)
        {
            return false;
        }
        _activeCoroutine = StartCoroutine(BecomeGoodCourotine(timeForCube));
        return true;
    }

    IEnumerator BecomeGoodCourotine(int timeForCube)
    {
        _cubeType = CubeType.Good;
        this.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
        yield return new WaitForSeconds(timeForCube);
        
        ResetCube();
    }

    public bool BecomeBad(int timeForCube)
    {
        if (_cubeType != CubeType.Normal)
        {
            return false;
        }
        _activeCoroutine = StartCoroutine(BecomeBadCourotine(timeForCube));
        return true;
    }

    IEnumerator BecomeBadCourotine(int timeForCube)
    {
        _cubeType = CubeType.Bad;
        this.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        
        yield return new WaitForSeconds(timeForCube);
        
        ResetCube();
    }

    void ResetCube()
    {
        _cubeType = CubeType.Normal;
        this.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
        _activeCoroutine = null;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.collider.name.StartsWith("Cube"))
        {
            if (_cubeType == CubeType.Good)
            {
                OnGoodButtonTouch?.Invoke();
            } else if (_cubeType == CubeType.Bad)
            {
                OnBadButtonTouch?.Invoke();
            }
        }
    }
}
