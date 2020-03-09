using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    private bool isStarted = false;

    [System.Serializable]
    public class Power
    {
        public string name;
        public float x;
        public float y;
        public GameObject[] PowerUp;
        public Transform[] points;
    }

    [SerializeField] Power[] power;

    private void Start()
    {
        isStarted = false;
    }

    private void Update()
    {
        if (!isStarted)
        {
            StartCoroutine(BulletPowerUp());
            StartCoroutine(HealthPowerUp());
        }
    }

    IEnumerator BulletPowerUp()
    {
        isStarted = true;
        while (isStarted == true)
        {
            float delay = Random.Range(power[0].x, power[0].y);
            yield return new WaitForSeconds(delay);
            int i = Random.Range(0, power[0].points.Length);
            int j = Random.Range(0, power[0].PowerUp.Length);
            Instantiate(power[0].PowerUp[j], power[0].points[i].position, Quaternion.identity);
        }
    }

    IEnumerator HealthPowerUp()
    {
        isStarted = true;
        while (isStarted == true)
        {
            float delay = Random.Range(power[0].x, power[0].y);
            yield return new WaitForSeconds(delay);
            int i = Random.Range(0, power[1].points.Length);
            int j = Random.Range(0, power[1].PowerUp.Length);
            Instantiate(power[1].PowerUp[0], power[1].points[i].position, Quaternion.identity);
        }
    }
}
