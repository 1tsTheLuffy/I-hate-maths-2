using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SummationBullet))]
public class SummationBullet : MonoBehaviour
{
    [SerializeField] private float speed;

    private Transform Player;

    private Vector2 Target;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Player = GameObject.FindGameObjectWithTag("Player").transform;
        Target = new Vector2(Player.position.x, Player.position.y);
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Target, speed * Time.deltaTime);

        if(transform.position.x == Target.x && transform.position.y == Target.y)
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
