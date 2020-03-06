using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(One))]
public class One : MonoBehaviour
{
    private int health;
    private Vector2 target;
    private float distance;
    [SerializeField] float speed;

    [SerializeField] GameObject destroyParticle;

    private CameraShake shake;

    private Transform bus;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        bus = GameObject.FindGameObjectWithTag("Player").transform;

        shake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();

        health = 1;
    }

    private void LateUpdate()
    {
        target = new Vector2(bus.position.x, bus.position.y);
    }

    private void Update()
    {

        distance = Vector2.Distance(transform.position, bus.position);

        if (health == 0)
        {
            Destroy(gameObject);
            GameObject instance = Instantiate(destroyParticle, transform.position, Quaternion.identity);
            Destroy(instance, 1.5f);
        }
    }

    private void FixedUpdate()
    {
        if (distance < 24f && (bus.position.x < transform.position.x))
        {
            transform.Rotate(0f, 0f, 8f);
            transform.position = Vector2.MoveTowards(transform.position, target, (speed * speed * 1.2f) * Time.fixedDeltaTime);
        }
        else
        {
            float randomUp = Random.Range(10f, 20f);
            rb.AddForce(Vector2.up * randomUp, ForceMode2D.Force);
            transform.Translate(Vector2.left * speed * 2 * Time.fixedDeltaTime);
            float randomDown = Random.Range(10f, 20f);
            rb.AddForce(Vector2.down * randomDown, ForceMode2D.Force);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet_1"))
        {
            Destroy(collision.transform.gameObject);
            shake.C_Shake(.1f, 2f, .8f);
            health = 0;
        }

        if(collision.CompareTag("Bullet_2"))
        {
            shake.C_Shake(.1f, 2f, .8f);
            health = 0;
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
