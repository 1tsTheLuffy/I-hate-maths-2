using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BossSpawner))]
public class BossSpawner : MonoBehaviour
{
    public GameObject boss;

    public Transform spawnPoint;

    private void Awake()
    {
        transform.gameObject.SetActive(false);
    }

    private void Start()
    {
        Instantiate(boss, spawnPoint.position, Quaternion.identity);
    }
}
