using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowValues : MonoBehaviour
{

    GameObject player;
    Rigidbody2D rb;
    Text values_text;
    PlayerController controller;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody2D>();
        values_text = GetComponent<Text>();
        controller = player.GetComponent<PlayerController>();
    }

    void FixedUpdate()
    {
        if (player)
        {
            float angle = Vector2.Angle(Vector2.right, new Vector2(rb.velocity.x, rb.velocity.y));
            if (rb.velocity.y < 0) angle = -angle;
    
            values_text.text = "speed: " + rb.velocity.magnitude + "\n" + "energy: " + player.GetComponent<Player>().GetEnergy();
        }
    }


}
