using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyGun : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collider)
    {
       if(collider.gameObject.name == "Player")
        {
            collider.gameObject.GetComponent<PlayerController>().PickUpGun();
            Destroy(gameObject);
        } 
    }

}
