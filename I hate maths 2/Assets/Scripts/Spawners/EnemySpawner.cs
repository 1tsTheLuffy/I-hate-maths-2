using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private bool isStarted;
    private bool isAlpha;
    private bool isCurlyStarted;
    private bool isCurly_2;
    private bool isDiagram;

    public PlayerScoreManager sm;

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
        if (sm == null)
            return;

        isStarted = false;
        isAlpha = false;
    }

    private void Update()
    {
        if (sm == null)
            return;

        if (!isStarted)
        {
            StartCoroutine(One());
        }

        if(!isCurlyStarted)
        {
            StartCoroutine(Curly());
        }

        if(!isAlpha)
        {
            StartCoroutine(Alpha());
        }

        if(!isCurly_2)
        {
            StartCoroutine(Curly_2());
        }

        if(!isDiagram)
        {
            StartCoroutine(Diagram());
        }


        if(sm.playerController.score > 0 && sm.playerController.score < 15)
        {
            getEnemyData[0].x = 1;
            getEnemyData[0].y = 4;

            getEnemyData[2].x = 1;
            getEnemyData[2].y = 4;
        }

        if(sm.playerController.score > 15 && sm.playerController.score < 30)
        {
            getEnemyData[0].x = 1.7f;
            getEnemyData[0].y = 3.7f;

            getEnemyData[2].x = 1.7f;
            getEnemyData[2].y = 3.7f;
        }

        if(sm.playerController.score > 30 && sm.playerController.score < 60)
        {
            getEnemyData[0].x = 1;
            getEnemyData[0].y = 5;

            getEnemyData[2].x = 1;
            getEnemyData[2].y = 5;
        }

        if(sm.playerController.score > 45 && sm.playerController.score < 65)
        {
            getEnemyData[1].x = 60;
            getEnemyData[1].y = 80;
        }

        //if(sm.playerController.score > 100)
        //{
        //    getEnemyData[4].x = 50;
        //    getEnemyData[4].y = 100;
        //}

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

    IEnumerator Alpha()
    {
        isAlpha = true;
        while(isAlpha == true)
        {
            float delay = Random.Range(getEnemyData[2].x, getEnemyData[2].y);
            yield return new WaitForSeconds(delay);
            int i = Random.Range(0, getEnemyData[2].EnemyPosition.Length);
            Instantiate(getEnemyData[2].Enemy[0], getEnemyData[2].EnemyPosition[i].position, Quaternion.identity);
        }
    }

    IEnumerator Curly_2()
    {
        isCurly_2 = true;
        while (isCurly_2 == true)
        {
            float delay = Random.Range(getEnemyData[3].x, getEnemyData[3].y);
            yield return new WaitForSeconds(delay);
            int i = Random.Range(0, getEnemyData[3].EnemyPosition.Length);
            Instantiate(getEnemyData[3].Enemy[0], getEnemyData[3].EnemyPosition[i].position, Quaternion.identity);
        }
    }

    IEnumerator Diagram()
    {
        isDiagram = true;
        while (isDiagram == true)
        {
            float delay = Random.Range(getEnemyData[4].x, getEnemyData[4].y);
            yield return new WaitForSeconds(delay);
            int i = Random.Range(0, getEnemyData[4].EnemyPosition.Length);
            Instantiate(getEnemyData[4].Enemy[0], getEnemyData[4].EnemyPosition[i].position, Quaternion.identity);
        }
    }
}
