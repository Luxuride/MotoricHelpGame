using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    public ParticleSystem MyParticleSystem;
    private bool CollisionProcessing = false;
    private int points = 0;
    public StoneSpawner stoneSpawner;

    public TextMeshPro pointsText;
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
        if (col.gameObject.name.StartsWith("GameRock") && !CollisionProcessing)
        {
            this.CollisionProcessing = true;
            StartCoroutine(DestroyObjectAfterDelay(col.gameObject));
        }
    }

    IEnumerator DestroyObjectAfterDelay(GameObject item)
    {
        yield return new WaitForSeconds(.5f);
        points++;
        pointsText.text = $"Body: {points}";
        Destroy(item);
        this.MyParticleSystem.Play();
        yield return new WaitForSeconds(1.5f);
        this.CollisionProcessing = false;
        this.stoneSpawner.CreateStone();
    }
}
