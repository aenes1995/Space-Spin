using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    GameObject player_obj;

	void Start () {
        if (GameObject.FindGameObjectWithTag("Player"))
            player_obj = GameObject.FindGameObjectWithTag("Player");	
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (player_obj)
        {
            transform.position = new Vector3(player_obj.transform.position.x, player_obj.transform.position.y, transform.position.z);
        }
	}
}
