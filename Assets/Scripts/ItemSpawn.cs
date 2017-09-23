using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour {

    public GameObject item;
    public bool loop;
    public float delay;
    private float nextSpawn;

    public void Start()
    {
        nextSpawn = Time.time;
        Spawn();
    }

    public void Update()
    {
        if(loop && Time.time > nextSpawn)
        {
            nextSpawn = Time.time + delay;
            Spawn();
        } 
    }

    private void Spawn()
    {
        Instantiate(item, transform.position, Quaternion.identity);
    }
}
