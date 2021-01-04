using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class LocalGameManager : MonoBehaviour
{
    public CubeScript[] Cubes;
    public int TimeBetweenGames = 3;

    public int TimeForCube = 2;

    public int BadCubes = 1;

    public TextMeshPro PointsText;

    private int points = 0;

    private Coroutine GameRoundCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        GameRoundCoroutine = StartCoroutine(PlayGameRound());
    }

    private void OnEnable()
    {
        CubeScript.OnGoodButtonTouch += OnGood;
        CubeScript.OnBadButtonTouch += OnBad;
    }

    private void OnDisable()
    {
        CubeScript.OnGoodButtonTouch -= OnGood;
        CubeScript.OnBadButtonTouch -= OnBad;
    }

    private void OnBad()
    {
        StopCoroutine(GameRoundCoroutine);
        GameRoundCoroutine = StartCoroutine(PlayGameRound());
        PointsText.color = Color.red;
    }

    private void OnGood()
    {
        points++;
        PointsText.text = $"Body: {points}";
        StopCoroutine(GameRoundCoroutine);
        GameRoundCoroutine = StartCoroutine(PlayGameRound());
    }

    private IEnumerator PlayGameRound()
    {
        yield return new WaitForSeconds(TimeBetweenGames);
        while (true)
        {
            Cubes[Random.Range(0, Cubes.Length)].BecomeGood(TimeForCube);
            int badCubes = 0;
            while (badCubes < BadCubes && badCubes < Cubes.Length - 1)
            {
                if (Cubes[Random.Range(0, Cubes.Length)].BecomeBad(TimeForCube))
                {
                    badCubes++;
                }
            }
            yield return new WaitForSeconds(TimeBetweenGames + TimeForCube);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
}
