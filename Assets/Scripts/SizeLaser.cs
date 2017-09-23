using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeLaser : MonoBehaviour {

    public string type;
    public Vector3 direction;
    public Material laserMaterial;
    public GameObject laserSpawn;
    public AudioSource laserNoise;

    void Start()
    {
        laserNoise = Instantiate(laserNoise);
    }

	void FixedUpdate () {
        RaycastHit2D hit = Physics2D.Raycast(laserSpawn.transform.position, direction, Mathf.Infinity);
        if (hit)
        {
            GameObject line = new GameObject();
            line.transform.position = transform.position;
            line.AddComponent<LineRenderer>();
            LineRenderer lr = line.GetComponent<LineRenderer>();
            lr.material = laserMaterial;
            lr.startWidth = 0.1f;
            lr.endWidth = 0.1f;
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, hit.point);
            GameObject.Destroy(line, 0.01f);
            if (hit.collider.gameObject.tag == "Player")
            {

                if (!laserNoise.isPlaying)
                {
                    laserNoise.Play();
                }
                if(type == "DOWN")
                {
                    hit.collider.gameObject.GetComponent<PlayerController>().Shrink();
                }
                if (type == "UP")
                {
                    hit.collider.gameObject.GetComponent<PlayerController>().Grow();
                } 
            }
        }
    }
}
