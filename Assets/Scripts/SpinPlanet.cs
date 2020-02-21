using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinPlanet : MonoBehaviour {

    public enum Direction { left = 1, right = -1};
    public Direction spinDirection = Direction.right;

    public float angularVelocity = 100f;

    Rigidbody2D rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.angularVelocity = (float)spinDirection * angularVelocity;
    }

}
