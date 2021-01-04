using System.Collections;
using TMPro;
using UnityEngine;

public class SphereSpawner : MonoBehaviour
{
    public int RespawnDelay = 3;
    private int points = 0;

    public TextMeshPro text;

    public GameObject SpherePrefab;
    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnSphere(GameObject sphere)
    {
        StartCoroutine(RespawnSphereCourutine(sphere));
    }

    private IEnumerator RespawnSphereCourutine(GameObject sphere)
    {
        Destroy(sphere);
        yield return new WaitForSeconds(RespawnDelay);
        Spawn();
    }

    public void Spawn()
    {
        var sphere = Instantiate(SpherePrefab, new Vector3(Random.Range(-1.5f, 0), .5f, -10.43f), Quaternion.identity);
        sphere.GetComponent<SphereScript>().sphereSpawner = this;
    }
    
    public void AddPoint()
    {
        points++;
        text.text = $"Body: {points}";
    }
}
