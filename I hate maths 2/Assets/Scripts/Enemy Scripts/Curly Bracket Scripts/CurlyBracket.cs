﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurlyBracket : MonoBehaviour
{
    private int health = 1;
    [SerializeField] float speed;
    [SerializeField] float frequency;
    [SerializeField] float magnitude;
    [SerializeField] float timer;
    [SerializeField] float timeBtwSpawn;

    [SerializeField] GameObject[] bullet;
    [SerializeField] GameObject destroyParticle;

    [SerializeField] Transform Point;
    [SerializeField] Transform[] shootPoint;

    Rigidbody2D rb;
    CameraShake shake;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        shake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();

        health = 1;
        timer = timeBtwSpawn;
    }

    private void Update()
    {
        if (timer <= 0)
        {
            Instantiate(bullet[0], shootPoint[0].position, shootPoint[0].rotation);
            Instantiate(bullet[1], shootPoint[1].position, shootPoint[1].rotation);
            timer = timeBtwSpawn;
        }
        else
        {
            timer -= Time.deltaTime;
        }

        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }

        if (health <= 0)
        {
            shake.C_Shake(.1f, 1f, 1f);
            GameObject instance = Instantiate(destroyParticle, transform.position, Quaternion.identity);
            Destroy(instance, 1.2f);
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        transform.Rotate(0f, 0f, 10f);
        // transform.RotateAround(transform, Vector3.forward, -25f * Time.fixedDeltaTime);
        Vector2 pos = transform.position;
        pos.y = Mathf.Tan(Time.time * frequency) * magnitude;
        transform.position = pos;
        //transform.Translate(Vector2.left * speed * Time.fixedDeltaTime);
        transform.position = Vector2.MoveTowards(transform.position, Point.position, speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet_1"))
        {
            Destroy(collision.transform.gameObject);
            shake.C_Shake(.01f, 1f, 1f);
            health = 0;
        }
    }
}
