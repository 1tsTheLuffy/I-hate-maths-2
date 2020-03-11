using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Boss_Bullet_1))]
public class Boss_Bullet_1 : MonoBehaviour
{
    [SerializeField] float fireSpeed;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Destroy(gameObject, 1.2f);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.up * fireSpeed * Time.fixedDeltaTime);
    }
}
