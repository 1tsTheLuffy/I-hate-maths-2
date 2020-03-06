using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assistant : MonoBehaviour
{
    public int assistantNum;

    [SerializeField] GameObject[] assistant;
    [SerializeField] GameObject powerUpTaken;

    [SerializeField] Transform assistantPoint;

    private void Start()
    {
        assistantNum = 0; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Assistant_PowerUp"))
        {
            if (assistantNum == 0)
            {
                Destroy(collision.transform.gameObject);
                GameObject instance = Instantiate(powerUpTaken, transform.position, Quaternion.identity);
                Instantiate(assistant[0], assistantPoint.position, Quaternion.identity);
                assistantNum = 1;
                Destroy(instance, 1f);
                return;
            }
        }
        if (collision.CompareTag("Assistant_PowerUp"))
        {
            if (assistantNum == 1)
            {
                Destroy(collision.transform.gameObject);
                GameObject instance = Instantiate(powerUpTaken, transform.position, Quaternion.identity);
                Instantiate(assistant[1], assistantPoint.position, Quaternion.identity);
                assistantNum = 2;
                Destroy(instance, 1f);
                return;
            }
        }

        if (collision.CompareTag("Assistant_PowerUp"))
        {
            if (assistantNum == 2)
            {
                Destroy(collision.transform.gameObject);
                return;
            }
        }
    }
}
