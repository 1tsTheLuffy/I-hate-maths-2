using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet_2 : MonoBehaviour
{
    [SerializeField] float speed = 15f;

    [SerializeField] GameObject destroyParticle;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Destroy(gameObject, 1f);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.left * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameObject instance = Instantiate(destroyParticle, transform.position, Quaternion.identity);
            Destroy(instance, 1.2f);
        }
    }
}
