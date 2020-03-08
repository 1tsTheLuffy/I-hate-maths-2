using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThetaSpawner : MonoBehaviour
{
    [SerializeField] float x = 1;
    [SerializeField] float y = 1;

    [SerializeField] GameObject Theta;
    [SerializeField] GameObject arrow;

    [SerializeField] Transform[] thetaSpawnPoints;

    private void Start()
    {
        StartCoroutine(Spawn());
    }
    
    IEnumerator Spawn()
    {
        while(true)
        {
            float delay = Random.Range(x, y);
            int i = Random.Range(0, thetaSpawnPoints.Length);
            yield return new WaitForSeconds(delay - .5f);
            GameObject instance = Instantiate(arrow, thetaSpawnPoints[i].position + new Vector3(3.5f, 0f, 0f), Quaternion.identity);
           // yield return new WaitForSeconds(.2f);
           // Destroy(instance);
            yield return new WaitForSeconds(delay);
            Destroy(instance);
            Instantiate(Theta, thetaSpawnPoints[i].position, Quaternion.identity);
        }
    }
}
