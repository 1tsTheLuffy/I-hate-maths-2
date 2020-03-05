﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assistant : MonoBehaviour
{
    public int assistantNum;

    [SerializeField] GameObject[] assistant;

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
                Instantiate(assistant[0], assistantPoint.position, Quaternion.identity);
                assistantNum = 1;
                return;
            }
        }
        if (collision.CompareTag("Assistant_PowerUp"))
        {
            if (assistantNum == 1)
            {
                Destroy(collision.transform.gameObject);
                Instantiate(assistant[1], assistantPoint.position, Quaternion.identity);
                assistantNum = 2;
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
