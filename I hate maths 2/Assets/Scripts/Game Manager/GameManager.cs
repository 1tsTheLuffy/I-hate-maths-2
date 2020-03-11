using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameManager))]
public class GameManager : MonoBehaviour
{
    public PlayerScoreManager sm;

    public GameObject enemySpawner;

    public GameObject bossSpawner;

    private void Start()
    {
        if(sm == null)
        {
            return;
        }
    }

    private void Update()
    {
        if(sm.playerController.score >= 150)
        {
            enemySpawner.SetActive(false);
            bossSpawner.gameObject.SetActive(true);
            return;
        }
    }
}