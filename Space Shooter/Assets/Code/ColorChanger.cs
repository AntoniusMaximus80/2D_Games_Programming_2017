﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour {

    public Color[] availableColors;
    SpriteRenderer spriteRenderer;
    float x, y, z;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        
    }

    // Use this for initialization
    void Start () {
        x = 0f;
        y = 0f;
        z = 0f;
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        Color newColor = spriteRenderer.color;
        newColor.r = newColor.r + Random.Range(-0.1f, 0.1f);
        if (newColor.r < 0f)
        {
            newColor.r = 0f;
        } else if (newColor.r > 1f)
        {
            newColor.r = 1f;
        }
        newColor.g = newColor.g + Random.Range(-0.1f, 0.1f);
        if (newColor.g < 0f)
        {
            newColor.g = 0f;
        }
        else if (newColor.g > 1f)
        {
            newColor.g = 1f;
        }
        newColor.b = newColor.b + Random.Range(-0.1f, 0.1f);
        if (newColor.b < 0f)
        {
            newColor.b = 0f;
        }
        else if (newColor.b > 1f)
        {
            newColor.b = 1f;
        }
        spriteRenderer.color = newColor;
        transform.localScale += new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0f);
        x += Time.deltaTime * 0f;
        y += Time.deltaTime * 0f;
        z += Time.deltaTime * 120f;
        transform.rotation = Quaternion.Euler(x, y, z);
    }

    private void FixedUpdate()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void OnDestroy()
    {
        
    }
}
