using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    LevelManager levelManager;
    BlackHole blackHole;

    private void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        blackHole = GameObject.FindGameObjectWithTag("BlackHole").GetComponent<BlackHole>();
    }
    
    void Update()
    {
        if (!GameObject.FindGameObjectWithTag("Player"))
        {
            if (levelManager.IsLevelCompleted() && blackHole.IsPassed())
            {
                if(Input.touchCount == 1)
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

        }
    }
}
