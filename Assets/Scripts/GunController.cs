using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    public LayerMask shotMask;
    public GameObject shotSpawner;
    public float power;
    public Material laserBigMaterial;
    public Material laserSmallMaterial;
    public AudioSource laserBig;
    public AudioSource laserSmall;

    void Start()
    {
        laserBig = Instantiate(laserBig);
        laserSmall = Instantiate(laserSmall);
    }
	
    public void FireBig(Vector3 mousePosition)
    {
        if (!laserBig.isPlaying)
        {
            laserBig.Play();
        }
        RaycastHit2D hit = Fire(mousePosition, laserBigMaterial);
        if (hit)
        {
            Changeable changeable = hit.collider.GetComponent<Changeable>();
            if (changeable)
            {
                changeable.AddSize(power);
            }
        }

    }

    public void FireSmall(Vector3 mousePosition)
    {
        if (!laserSmall.isPlaying)
        {
            laserSmall.Play();
        }
        RaycastHit2D hit = Fire(mousePosition, laserSmallMaterial);
        if (hit)
        {
            Changeable changeable = hit.collider.GetComponent<Changeable>();
            if (changeable)
            {
                changeable.AddSize(-power);
            }
        }
    }
    
    private RaycastHit2D Fire(Vector3 mousePosition, Material material)
    {
        RaycastHit2D hit = CastBeam(mousePosition);
        Vector3 hitFinal = mousePosition;
        if (hit)
        {
            hitFinal = hit.point; 
        }
        DrawFireLine(hitFinal, material);
        return hit;
    }

    private RaycastHit2D CastBeam(Vector3 mousePosition)
    {
        Vector3 direction = mousePosition - shotSpawner.transform.position;
        Vector3 endpoint = Vector3.Normalize(direction) * 3;  
        RaycastHit2D hit = Physics2D.Raycast(shotSpawner.transform.position, endpoint, Mathf.Infinity, shotMask);
        Debug.Log(hit.collider.gameObject.name);
        Debug.DrawRay(shotSpawner.transform.position, direction, Color.green);
        return hit;
    }

    private void DrawFireLine(Vector3 end, Material material)
    {
        GameObject line = new GameObject();
        line.transform.position = shotSpawner.transform.position;
        line.AddComponent<LineRenderer>();
        LineRenderer lr = line.GetComponent<LineRenderer>();
        lr.material = material; 
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.SetPosition(0, shotSpawner.transform.position);
        lr.SetPosition(1, end);
        GameObject.Destroy(line, 0.01f);
    }

}
