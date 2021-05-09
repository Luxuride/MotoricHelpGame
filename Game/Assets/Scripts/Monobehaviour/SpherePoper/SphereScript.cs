﻿using UnityEngine;

public class SphereScript : MonoBehaviour
{
    private bool collided = false;
    
    public float AntiGravity = 8f;
    public Rigidbody rb;

    public SphereSpawner sphereSpawner;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0,  Random.Range(2.7f, 3f), 3f);
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(AntiGravity * Time.deltaTime * Vector3.up, ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!collided)
        {
            collided = true;
            if (other.collider.name != "rpgpp_lt_terrain_grass_01" && !other.collider.name.Contains("Cube"))
            {
                sphereSpawner.AddPoint();
            }
            sphereSpawner.RespawnSphere(this.gameObject);
        }
    }
}
