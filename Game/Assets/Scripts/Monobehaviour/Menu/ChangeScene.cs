using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MinigameType
{
    Collision,
    Sticky,
    Grab
}
public class ChangeScene : MonoBehaviour
{
    public int GameSceneIndex = 0;
    
    private bool loadingScene = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.name);
        if (!loadingScene)
        {
            loadingScene = true;
            SceneManager.LoadSceneAsync(GameSceneIndex).completed += OnSceneLoaded();
        }
    }

    Action<UnityEngine.AsyncOperation> OnSceneLoaded()
    {
        loadingScene = false;
        return null;
    }
}
