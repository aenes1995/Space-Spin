using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {

    //public AnimationCurve factor;
    public float factor = 0.75f;

    GameObject player;
    float startRadius;
    float currentRadius;

    bool zoom = false;

    float z2;
    CircleCollider2D col;
    float startZ;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        col = player.GetComponent<CircleCollider2D>();
        startRadius = col.bounds.size.x / 2;
        currentRadius = startRadius;
        startZ = transform.position.z;
    }

    float Zoom(float z1, float z2, float velocity)
    {
        float value;
        if (Mathf.Abs(z2 - z1) > 0.01f)
        {
            value = Mathf.Lerp(z1, z2, velocity);
        }
        else
        {
            value = z2;
            this.zoom = false;
        }
        return value;
    }

    void Update()
    {
        if (player)
        {
            currentRadius = (float)col.bounds.size.x / 2;
            z2 = startZ * currentRadius / (startRadius);
        }
    }

    void FixedUpdate () {

        if (player)
        {
            if (Mathf.Abs(currentRadius - startRadius) > 0.1f && !zoom)
            {
                zoom = true;
            }

            if (zoom)
            {
                float value = Zoom(transform.position.z, z2 * factor, 0.1f);
                transform.position = new Vector3(transform.position.x, transform.position.y, value);
            }
        }

	}
}
