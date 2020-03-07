using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Second_CurlyBracket))]
public class Second_CurlyBracket : MonoBehaviour
{
    private int randomMovepoint = 0;
    [SerializeField] float speed = 5f;
    [SerializeField] float timer = 1f;
    [SerializeField] float timeBtwShoot = 1f;
    [SerializeField] float health = 4f;

    private Vector2 playerPos;// = Vector2.zero;

    [SerializeField] GameObject[] bullet;

    [SerializeField] Transform[] movepoint;
    [SerializeField] Transform[] shootPoint;
    Transform player;

    Rigidbody2D rb;
    CameraShake shake;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        shake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        timer = timeBtwShoot;

        randomMovepoint = Random.Range(0, movepoint.Length);
    }

    private void Update()
    {
        transform.Rotate(0f, 0f, 4f);

        float distance = Vector2.Distance(transform.position, movepoint[randomMovepoint].position);

        transform.position = Vector2.MoveTowards(transform.position, movepoint[randomMovepoint].position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, movepoint[randomMovepoint].position) < .2f)
        {
            randomMovepoint = Random.Range(0, movepoint.Length);
        }


        if (timer <= 0)
        {
            Instantiate(bullet[0], shootPoint[0].position, shootPoint[0].rotation);
            Instantiate(bullet[1], shootPoint[1].position, shootPoint[1].rotation);
            timer = timeBtwShoot;
        }
        else
        {
            timer -= Time.deltaTime;
        }   

        if(health <= 0)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet_1"))
        {
            Destroy(collision.transform.gameObject);
            health -= 1f;
            shake.C_Shake(0.1f, 1.5f, .8f);
        }
    }
}
