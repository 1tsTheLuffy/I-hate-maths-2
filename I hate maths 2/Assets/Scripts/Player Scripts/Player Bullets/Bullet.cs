using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 15f;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(transform.position.x > 7.5f)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.fixedDeltaTime);
    }
}
