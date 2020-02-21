using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowLevelCompleted : MonoBehaviour
{
    LevelManager levelManager;
    BlackHole blackHole;
    Text text;
    void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        blackHole = GameObject.FindGameObjectWithTag("BlackHole").GetComponent<BlackHole>();
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (levelManager.IsLevelCompleted() && !levelManager.IsGameOver() && blackHole.IsPassed()) text.enabled = true;
    }
}
