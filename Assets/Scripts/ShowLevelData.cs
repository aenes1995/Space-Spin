using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowLevelData : MonoBehaviour
{
    GameObject player;
    GameObject levelManager;
    Text level_text;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        levelManager = GameObject.Find("LevelManager");
        level_text = GetComponent<Text>();
    }

    void FixedUpdate()
    {
        if (player)
        {

            int count = levelManager.GetComponent<LevelManager>().GetOrbCount() - player.GetComponent<Player>().GetOrbCount();
            level_text.text = "Level N" + "\n" + "Energy Limit: " + levelManager.GetComponent<LevelManager>().EnergyLimit + "\n" + "Remaining Orb: " + count;

        }
    }
}
