using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public float EnergyLimit = 50;
    
    private GameObject player;
    private int orbCount;

    private bool levelCompleted = false;
    private bool gameOver = false;

    public int GetOrbCount()
    {
        return orbCount;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public bool IsLevelCompleted()
    {
        return levelCompleted;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        orbCount = GameObject.FindGameObjectsWithTag("Orb").Length;
    }
    private void Update()
    {
        if (player)
        {
            if(player.GetComponent<Player>().GetOrbCount() == orbCount)
            {
                if(player.GetComponent<Player>().GetEnergy() >= EnergyLimit)
                {
                    levelCompleted = true;
                }
                else
                {
                    gameOver = true;
                    Destroy(player);    
                }
            }
        }
    }

}
