using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Changeable : MonoBehaviour {

    public float minSize;
    public float maxSize;
    public float currentSize = 0;
    public bool isSmallBox = false;
    public bool isPlant = false;
    public GameObject pivot;
    private float sizeMultiplier = 2;
    private Vector3 pivotOrigin;
    private Vector3 originalScale;

    public void Start()
    {
        originalScale = transform.localScale;
        if (isSmallBox)
        {
            sizeMultiplier = 1;
        }
        else
        {
            sizeMultiplier = 2;
        }
        if (isPlant)
        {
            sizeMultiplier *= 2;
        }
        GetComponent<Rigidbody2D>().mass = currentSize * 1.5f;
        transform.localScale = new Vector3(currentSize, currentSize);
        GetComponent<Rigidbody2D>().mass = currentSize * sizeMultiplier;
    }

    public void AddSize(float size) {
        if(currentSize + size <= maxSize && currentSize + size >= minSize && GetComponent<Rigidbody2D>())
        {
            currentSize += size;
            transform.localScale += new Vector3(size, size);
            GetComponent<Rigidbody2D>().mass = currentSize * sizeMultiplier;
        }

    }

}
