using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public float minX;
    public float minY;
    public float maxX;
    public float maxY;

	// Update is called once per frame
	void Update () {
        Camera camera = GetComponent<Camera>();
        camera.transform.position = new Vector3(
            Mathf.Clamp(player.transform.position.x, minX, maxX), 
            Mathf.Clamp(player.transform.position.y, minY, maxY), 
            camera.transform.position.z
        );
	}
}
