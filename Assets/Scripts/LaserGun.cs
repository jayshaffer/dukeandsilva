using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour {

    public float shotDelay;
    public GameObject bolt;
    private float nextShot;

	// Use this for initialization
	void Start () {
        nextShot = Time.time + shotDelay;	
	}
	
	// Update is called once per frame
	void Update () {
	    if(nextShot < Time.time)
        {
            nextShot = Time.time + shotDelay;
            GameObject boltClone = Instantiate(bolt, new Vector3(transform.position.x - 1, transform.position.y, transform.position.z), Quaternion.identity);
        }	
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("testshot"); 
    }
}
