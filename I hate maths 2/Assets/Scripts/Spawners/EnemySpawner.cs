using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private bool isStarted;
    private bool isCurlyStarted;

    [System.Serializable]
    public class GetEnemyData
    {
        public string name;
        public float x;
        public float y;
        public GameObject[] Enemy;
        public Transform[] EnemyPosition;
    }

    [SerializeField] GetEnemyData[] getEnemyData;

    private void Start()
    {
        isStarted = false;
    }

    private void Update()
    {
        if (!isStarted)
        {
            StartCoroutine(One());
        }

        if(!isCurlyStarted)
        {
            StartCoroutine(Curly());
        }
    }

    IEnumerator One()
    {
        isStarted = true;
        while (isStarted == true)
        {
            float delay = Random.Range(getEnemyData[0].x, getEnemyData[0].y);
            int i = Random.Range(0, getEnemyData[0].EnemyPosition.Length);
            Instantiate(getEnemyData[0].Enemy[0], getEnemyData[0].EnemyPosition[i].position, Quaternion.identity);
            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator Curly()
    {
        isCurlyStarted = true;
        while (isCurlyStarted == true)
        {
            float delay = Random.Range(getEnemyData[1].x, getEnemyData[1].y);
            yield return new WaitForSeconds(delay);
            int i = Random.Range(0, getEnemyData[1].EnemyPosition.Length);
            Instantiate(getEnemyData[1].Enemy[0], getEnemyData[1].EnemyPosition[i].position, Quaternion.identity);
        }
    }
}
