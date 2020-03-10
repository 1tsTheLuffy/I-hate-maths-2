using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxSpawner : MonoBehaviour
{
    [SerializeField] GameObject parent;
    [SerializeField] GameObject BG;

    [SerializeField] Transform spawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("New"))
        {
            GameObject instance = Instantiate(BG, spawnPoint.position, Quaternion.identity);
            instance.transform.parent = parent.transform;
            Debug.Log("Hit!!");
        }
    }
}
