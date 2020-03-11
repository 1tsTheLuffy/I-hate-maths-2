using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(GameSceneManager))]
public class GameSceneManager : MonoBehaviour
{
    public void LoadMainScene()
    {
        SceneManager.LoadScene(01);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
