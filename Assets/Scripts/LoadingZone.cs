using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingZone : MonoBehaviour {

    public string sceneName;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject && collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    } 
}
