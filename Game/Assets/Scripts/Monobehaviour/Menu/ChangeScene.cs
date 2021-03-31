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
        StartCoroutine(EnableCollision());
    }

    // Fix bug of throwing player into all scenes at the same time if hands are visible
    private bool CollisionEnabled = false;
    IEnumerator EnableCollision()
    {
        yield return new WaitForSeconds(0.1f);
        CollisionEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!loadingScene && CollisionEnabled)
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
