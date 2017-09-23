using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBolt : MonoBehaviour {



	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(-.2f, 0));
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(collider.gameObject.name);
        if (collider.gameObject.name != "Laser Gun" && collider.gameObject.name != "Collision")
        {
            Destroy(this.gameObject);
            if(collider.gameObject.tag == "Player")
            {
                collider.gameObject.GetComponent<PlayerController>().Die();
            }
        }
        
     }

}
