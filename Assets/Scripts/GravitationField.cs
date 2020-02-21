using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitationField : MonoBehaviour {

    public float factor = 5f;
    float mass = 2f;
    GameObject parent;

	void Start () {
        parent = transform.parent.gameObject;
        if (parent.GetComponent<Planet>()) mass = parent.GetComponent<Planet>().resourceSettings.planetMass;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject != parent && other.gameObject.tag == "Player" && other.GetComponent<Rigidbody2D>())
        {

            Rigidbody2D other_rb = other.GetComponent<Rigidbody2D>();
            Vector3 subs = other.transform.position - transform.position;

            float G = 6.67408f;

            Vector2 gravitation_force = new Vector2(-subs.x, -subs.y);

            float dist = gravitation_force.magnitude;
            float forceMagnitude = factor*G * (mass) / dist;

            gravitation_force = forceMagnitude * gravitation_force.normalized;
            other_rb.AddForce(gravitation_force, ForceMode2D.Force);
            
        }

    }

    private void Update()
    {
        //transform.position = parent.transform.position;
    }

}
