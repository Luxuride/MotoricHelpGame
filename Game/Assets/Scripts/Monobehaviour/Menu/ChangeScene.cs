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
    public Scene GameScene;

    private bool loadingScene = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider);
        if (!loadingScene)
        {
            loadingScene = true;
            SceneManager.LoadSceneAsync("Collision1").completed += OnSceneLoaded();
        }
    }

    Action<UnityEngine.AsyncOperation> OnSceneLoaded()
    {
        loadingScene = false;
        return null;
    }
}
