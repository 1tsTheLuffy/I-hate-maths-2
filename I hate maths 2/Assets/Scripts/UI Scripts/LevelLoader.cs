using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(LevelLoader))]
public class LevelLoader : MonoBehaviour
{
    public Animator animator;

    public void LoadNextLevel(int index)
    {
        StartCoroutine(loadLevel(index));
    }

    public IEnumerator loadLevel(int index)
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(index);
    }
}
