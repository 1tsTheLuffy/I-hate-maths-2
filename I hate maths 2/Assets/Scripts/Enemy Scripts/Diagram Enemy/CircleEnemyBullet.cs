using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleEnemyBullet))]
public class CircleEnemyBullet : MonoBehaviour
{
    [SerializeField] float speed;

    private Vector2 moveDirection;

   // [SerializeField] GameObject destroyParticle;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // InvokeRepeating("Disable", 0, 3f);
    }

    private void FixedUpdate()
    {
        transform.Translate(moveDirection * speed * Time.fixedDeltaTime);
    }

    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}