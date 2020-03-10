using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    public int score;

    public PlayerController playerController;

    private void Start()
    {
        if(playerController == null)
        {
            return;
        }

        scoreText.text = playerController.score.ToString();
    }

    private void Update()
    {
        if(playerController == null)
        {
            return;
        }

        score = playerController.score;

        scoreText.text = playerController.score.ToString();
    }
}
