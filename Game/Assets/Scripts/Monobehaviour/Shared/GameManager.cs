using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else if (Instance != this)
        {
            Debug.Log("GameManager instance already exists. Destroying.");
            Destroy(this);
        }
    }

    private void Start()
    {
        Client.Instance.ConnectToServer();
    }
}
